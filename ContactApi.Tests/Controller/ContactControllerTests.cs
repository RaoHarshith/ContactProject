using System.Threading.Tasks;
using ContactApi.Controllers;
using ContactApi.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactApi.Tests.Controller
{
	// I am writting a single example due to time limitations 
	[TestClass]
    public class ContactControllerTests
    {
		[TestMethod]
		public async Task Get_Calls_ListAsync()
		{
			// Given I have mocked my manager
			var moqContactManager = new Mock<IContactManager>();

			// And I have instantiated my controller
			var controller = CreateController(moqContactManager: moqContactManager);

			// When I call the controller
			var result = await controller.Get();

			// Then I expect that ListAsync was called once with/without the correct parameters
			moqContactManager.Verify(m => m.ListAsync(), Times.Once);
		}

		private ContactsController CreateController(IMock<IContactManager> moqContactManager = null)
		{
			return new ContactsController(moqContactManager.Object);
		}
    }
}