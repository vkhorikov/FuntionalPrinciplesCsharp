using System.Data;
using System.Data.SqlClient;
using CustomerManagement.Logic.Utils;

namespace CustomerManagement.Tests.Integration
{
    public abstract class Tests
    {
        private const string ConnectionString = @"Server=.;Database=CustomerManagement;Trusted_Connection=true;";

        protected Tests()
        {
            ClearDatabase();

            Initer.Init(ConnectionString);
        }

        private void ClearDatabase()
        {
            string query = 
                "DELETE FROM dbo.Customer;" + 
                "UPDATE dbo.Ids SET NextHigh = 0";

            using (var cnn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(query, cnn)
                {
                    CommandType = CommandType.Text
                };

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //protected void FlushFakeEmails()
        //{
        //    var provider = (FakeEmailProvider)EmailService.Instance.EmailProvider;
        //    provider.FlushEmails();
        //}
    }
}
