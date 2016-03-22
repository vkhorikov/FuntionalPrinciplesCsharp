using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using CustomerManagement.Api.Models;
using CustomerManagement.Logic.Common;
using CustomerManagement.Logic.Model;
using CustomerManagement.Logic.Utils;

namespace CustomerManagement.Api.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomerRepository _customerRepository;
        private readonly IEmailGateway _emailGateway;
        private readonly IndustryRepository _industryRepository;

        public CustomerController(UnitOfWork unitOfWork, IEmailGateway emailGateway)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = new CustomerRepository(unitOfWork);
            _industryRepository = new IndustryRepository(unitOfWork);
            _emailGateway = emailGateway;
        }

        [HttpPost]
        [Route("customers")]
        public HttpResponseMessage Create(CreateCustomerModel model)
        {
            try
            {
                ValidateName(model.Name);
                ValidateEmail(model.PrimaryEmail, "Primary email");
                if (model.SecondaryEmail != null)
                {
                    ValidateEmail(model.SecondaryEmail, "Secondary email");
                }

                Industry industry = _industryRepository.GetByName(model.Industry);
                if (industry == null)
                    throw new BusinessException("Industry name is invalid: " + model.Industry);

                var customer = new Customer(model.Name, model.PrimaryEmail, model.SecondaryEmail, industry);
                _customerRepository.Save(customer);

                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Envelope.Ok());
            }
            catch (BusinessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Envelope.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Envelope.Error(ex.Message));
            }
        }

        private void ValidateEmail(string email, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new BusinessException(fieldName + " should not be empty");
            if (email.Length > 256)
                throw new BusinessException(fieldName + " is too long");
            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                throw new BusinessException(fieldName + " is invalid");
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Customer name should not be empty");
            if (name.Length > 200)
                throw new BusinessException("Customer name is too long");
        }

        [HttpPut]
        [Route("customers/{id}")]
        public HttpResponseMessage Update(UpdateCustomerModel model)
        {
            try
            {
                Customer customer = _customerRepository.GetById(model.Id);
                if (customer == null)
                    throw new BusinessException("Customer with such Id is not found: " + model.Id);

                Industry industry = _industryRepository.GetByName(model.Industry);
                if (industry == null)
                    throw new BusinessException("Industry name is invalid: " + model.Industry);

                customer.UpdateIndustry(industry);

                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Envelope.Ok());
            }
            catch (BusinessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Envelope.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Envelope.Error(ex.Message));
            }
        }

        [HttpDelete]
        [Route("customers/{id}/emailing")]
        public HttpResponseMessage DisableEmailing(long id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                    throw new BusinessException("Customer with such Id is not found: " + id);

                customer.DisableEmailing();

                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Envelope.Ok());
            }
            catch (BusinessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Envelope.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Envelope.Error(ex.Message));
            }
        }

        [HttpGet]
        [Route("customers/{id}")]
        public HttpResponseMessage Get(long id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                    throw new BusinessException("Customer with such Id is not found: " + id);

                return Request.CreateResponse(HttpStatusCode.OK, Envelope.Ok(customer));
            }
            catch (BusinessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Envelope.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Envelope.Error(ex.Message));
            }
        }

        [HttpPost]
        [Route("customers/{id}/promotion")]
        public HttpResponseMessage Promote(long id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                    throw new BusinessException("Customer with such Id is not found: " + id);

                if (!customer.CanBePromoted())
                    throw new BusinessException("The customer has the highest status possible");

                customer.Promote();
                _emailGateway.SendPromotionNotification(customer.PrimaryEmail, customer.Status);

                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Envelope.Ok());
            }
            catch (BusinessException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Envelope.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Envelope.Error(ex.Message));
            }
        }
    }
}
