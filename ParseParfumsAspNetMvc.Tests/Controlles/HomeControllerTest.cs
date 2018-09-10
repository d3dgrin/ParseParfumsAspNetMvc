using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParseParfumsAspNetMvc.Controllers;
using ParseParfumsAspNetMvc.Interfaces;
using ParseParfumsAspNetMvc.Models;

namespace ParseParfumsAspNetMvc.Tests.Controlles
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(a => a.GetAll()).Returns(new List<Product>());
            HomeController controller = new HomeController(mock.Object);
        }
    }
}
