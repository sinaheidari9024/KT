namespace KT.UnitTests.Application
{
    public class AddExtraDataCommandTests
    {
        [Test]
        public void AddExtraDatar_Failed_CustomerMobileNumberExistsException()
        {
            // Arrange
            AddExtraDataCommand command = new AddExtraDataCommand("Robbie Stwart"
                            , "123456789", "09876543211", "1", "test@gmail.com");

            var mockCustomerRepository = new Mock<IRepository<Customer>>();
            var mockCacheService = new Mock<ICacheService>();

            mockCustomerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None))
                                        .ReturnsAsync(GenerateData.GetCustmerWithSameMobile());
            mockCacheService.Setup(x => x.TryToGetData<CacheDTO>(It.IsAny<string>())).Returns(true);
            var handler = new AddExtraDataCommandHandler(mockCustomerRepository.Object, mockCacheService.Object);

            // Act
            // Assert
            var exception = Assert.ThrowsAsync<CustomerMobileNumberExistsException>(async () => await handler.Handle(command, CancellationToken.None));
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo(GenerateData.GetMobileNumberExistsMessage(command.Mobile)));
        }

        [Test]
        public void AddExtraDatar_Failed_CustomerEmailAddressExistsException()
        {
            // Arrange
            AddExtraDataCommand command = new AddExtraDataCommand("Robbie Stwart"
                            , "123456789", "09876543211", "1", "test@gmail.com");

            var mockCustomerRepository = new Mock<IRepository<Customer>>();
            var mockCacheService = new Mock<ICacheService>();

            mockCustomerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None))
                                        .ReturnsAsync(GenerateData.GetCustmerWithSameEmail());
            mockCacheService.Setup(x => x.TryToGetData<CacheDTO>(It.IsAny<string>())).Returns(true);
            var handler = new AddExtraDataCommandHandler(mockCustomerRepository.Object, mockCacheService.Object);

            // Act
            // Assert
            var exception = Assert.ThrowsAsync<CustomerEmailAddressExistsException>(async () => await handler.Handle(command, CancellationToken.None));
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo(GenerateData.GetEmailExistsMessage(command.Email)));
        }

        [Test]
        public void AddExtraDatar_Failed_UnexceptedStateInCache()
        {
            // Arrange
            AddExtraDataCommand command = new AddExtraDataCommand("Robbie Stwart"
                            , "123456789", "09876543211", "1", "test@gmail.com");

            var mockCustomerRepository = new Mock<IRepository<Customer>>();
            var mockCacheService = new Mock<ICacheService>();

            mockCustomerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None))
                                        .ReturnsAsync(GenerateData.GetCustomer());
            mockCacheService.Setup(x => x.TryToGetData<CacheDTO>(It.IsAny<string>())).Returns(false);
            var handler = new AddExtraDataCommandHandler(mockCustomerRepository.Object, mockCacheService.Object);

            // Act
            // Assert
            var exception = Assert.ThrowsAsync<TryAgainException>(async () => await handler.Handle(command, CancellationToken.None));
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo(GenerateData.GetTryAgainMessage()));
        }

        [Test]
        public void AddExtraData_Failed_DataIsNotInCache()
        {
            // Arrange
            AddExtraDataCommand command = new AddExtraDataCommand("Robbie Stwart"
                            , "123456789", "09876543211", "1", "test@gmail.com");

            var mockCustomerRepository = new Mock<IRepository<Customer>>();
            var mockCacheService = new Mock<ICacheService>();

            mockCustomerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None))
                                        .ReturnsAsync(GenerateData.GetCustomer());
            mockCacheService.Setup(x => x.TryToGetData<CacheDTO>(It.IsAny<string>())).Returns(false);
            mockCacheService.Setup(x => x.GetData<CacheDTO>(It.IsAny<string>())).Returns(GenerateData.GetUnexceptedStateCacheData());

            var handler = new AddExtraDataCommandHandler(mockCustomerRepository.Object, mockCacheService.Object);

            // Act
            // Assert
            var exception = Assert.ThrowsAsync<TryAgainException>(async () => await handler.Handle(command, CancellationToken.None));
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo(GenerateData.GetTryAgainMessage()));
        }


        [Test]
        public async Task AddExtraData_Success_NoExceptionOccours()
        {
            // Arrange
            AddExtraDataCommand command = new AddExtraDataCommand("Robbie Stwart"
                            , "123456789", "09876543211", "1", "test@gmail.com");

            var mockCustomerRepository = new Mock<IRepository<Customer>>();
            var mockCacheService = new Mock<ICacheService>();

            mockCustomerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None))
                                        .ReturnsAsync(GenerateData.GetCustomer());
            mockCacheService.Setup(x => x.TryToGetData<CacheDTO>(It.IsAny<string>())).Returns(true);
            mockCacheService.Setup(x => x.GetData<CacheDTO>(It.IsAny<string>())).Returns(GenerateData.GetCacheData());
            mockCacheService.Setup(x => x.SetData(It.IsAny<string>(), It.IsAny<CacheDTO>(), It.IsAny<DateTimeOffset>())).Returns(true);

            var handler = new AddExtraDataCommandHandler(mockCustomerRepository.Object, mockCacheService.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockCacheService.Verify(x => x.GetData<CacheDTO>(It.IsAny<string>()), Times.Once());
            mockCacheService.Verify(x => x.SetData<CacheDTO>(It.IsAny<string>(), It.IsAny<CacheDTO>(), It.IsAny<DateTimeOffset>()), Times.Once());
            mockCustomerRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Customer>>(), CancellationToken.None), Times.Once());
        }
    }
}
