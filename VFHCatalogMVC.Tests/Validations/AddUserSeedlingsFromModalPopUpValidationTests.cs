using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.User;
using Xunit;

namespace Application.UnitTests.Validations
{
    public class AddUserSeedlinglingsFromModalPopUpValidationTests
    {
        [Fact]
        public void Add_NewUserSeedlinglingFromModalPopUp_ProperReqest_ShouldNotReturnValidationError()
        {
            var validator = new UserSeedlingVm.UserSeedlingValidation();
            var newSeedling = new UserSeedlingVm
            {
                Id = 1,
                PlantId = 1,
                Count = 10,
                Description = "Test",
                UserId = "test",
            };

            validator.TestValidate(newSeedling).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Add_NewUserSeedlinglingFromModalPopUp_InvalidReqest_WrongId_ShouldReturnValidationError()
        {
            var validator = new UserSeedlingVm.UserSeedlingValidation();
            var newSeedling = new UserSeedlingVm
            {
                Id = 0,

            };

            validator.TestValidate(newSeedling).ShouldHaveValidationErrorFor(nameof(newSeedling.Id));
        }

        [Fact]
        public void Add_NewUserSeedlingFromModalPopUp_InvalidReqest_WrongPlantId_ShouldReturnValidationError()
        {
            var validator = new UserSeedlingVm.UserSeedlingValidation();
            var newSeedling = new UserSeedlingVm
            {

                PlantId = 0,

            };

            validator.TestValidate(newSeedling).ShouldHaveValidationErrorFor(nameof(newSeedling.PlantId));
        }

        [Fact]
        public void Add_NewUserSeedlingFromModalPopUp_InvalidReqest_WrongCount_ShouldReturnValidationError()
        {
            var validator = new UserSeedlingVm.UserSeedlingValidation();
            var newSeedling = new UserSeedlingVm
            {
                Count = 0,

            };

            validator.TestValidate(newSeedling).ShouldHaveValidationErrorFor(nameof(newSeedling.Count));
        }

        [Fact]
        public void Add_NewUserSeedlingFromModalPopUp_InvalidReqest_EmptyDescription_ShouldReturnValidationError()
        {
            var validator = new UserSeedlingVm.UserSeedlingValidation();
            var newSeedling = new UserSeedlingVm
            {

                Description = string.Empty,

            };

            validator.TestValidate(newSeedling).ShouldHaveValidationErrorFor(nameof(newSeedling.Description));
        }
    }
}
