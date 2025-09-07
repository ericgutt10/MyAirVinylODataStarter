using CustomersODataApp.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace CustomersODataApp
{
    public class EdmModelBuilder
    {
        // Learn more about OData Model Builder: https://learn.microsoft.com/odata/webapi/model-builder-abstract
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "NS";
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders");

            var customerType = builder.EntityType<Customer>();

            // Define the Bound function to a single entity
            customerType
                .Function("GetCustomerOrdersTotalAmount")
                .Returns<int>();

            // Define theBound function to collection
            customerType
                .Collection
                .Function("GetCustomerByName")
                .ReturnsFromEntitySet<Customer>("Customers")
                .Parameter<string>("name");

            return builder.GetEdmModel();
        }
    }
}