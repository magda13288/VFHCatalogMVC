using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.User;
using Xunit;

namespace Application.UnitTests.Validations
{
    public class AddUserSeedFromModalPopUpValidationTests
    {
        [Fact]
        public void Add_NewUserSeedFromModalPopUp_ProperReqest_ShouldNotReturnValidationError()
        {
            var validator = new UserSeedVm.UserSeedValidation();
            var newSeed = new UserSeedVm
            {
                Id = 1,
                PlantId = 1,
                Count = 10,
                Description = "Test",
                UserId = "test",
            };

            validator.TestValidate(newSeed).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Add_NewUserSeedFromModalPopUp_InvalidReqest_WrongId_ShouldReturnValidationError()
        {
            var validator = new UserSeedVm.UserSeedValidation();
            var newSeed = new UserSeedVm
            {
                Id = -1,
               
            };

            validator.TestValidate(newSeed).ShouldHaveValidationErrorFor(nameof(newSeed.Id));
        }

        [Fact]
        public void Add_NewUserSeedFromModalPopUp_InvalidReqest_WrongPlantId_ShouldReturnValidationError()
        {
            var validator = new UserSeedVm.UserSeedValidation();
            var newSeed = new UserSeedVm
            {
             
                PlantId = 0,
               
            };

            validator.TestValidate(newSeed).ShouldHaveValidationErrorFor(nameof(newSeed.PlantId));
        }

        [Fact]
        public void Add_NewUserSeedFromModalPopUp_InvalidReqest_WrongCount_ShouldReturnValidationError()
        {
            var validator = new UserSeedVm.UserSeedValidation();
            var newSeed = new UserSeedVm
            {         
                Count = 0,
              
            };

            validator.TestValidate(newSeed).ShouldHaveValidationErrorFor(nameof(newSeed.Count));
        }

        [Fact]
        public void Add_NewUserSeedFromModalPopUp_InvalidReqest_EmptyDescription_ShouldReturnValidationError()
        {
            var validator = new UserSeedVm.UserSeedValidation();
            var newSeed = new UserSeedVm
            {
                
                Description = string.Empty,
                
            };

            validator.TestValidate(newSeed).ShouldHaveValidationErrorFor(nameof(newSeed.Description));
        }

        //[Fact]
        //public void Add_NewUserSeedFromModalPopUp_ProperReqest_ShouldNotReturValidationException()
        //{
        //    var validator = new UserSeedVm.UserSeedValidation();
        //    var newSeed = new UserSeedVm
        //    {

        //        DateAdded = DateTime.Now,

        //    };

        //    validator.TestValidate(newSeed).ShouldNotHaveAnyValidationErrors();
        //}

        //[Fact]
        //public void Add_NewUserSeedFromModalPopUp_InvalidReqest_WrongDateAdded_ShoulReturValidationException()
        //{
        //    var validator = new UserSeedVm.UserSeedValidation();
        //    var newSeed = new UserSeedVm
        //    {

        //        DateAdded = DateTime.Now.AddMinutes(10),

        //    };

        //    validator.TestValidate(newSeed).ShouldHaveValidationErrorFor(nameof(newSeed.DateAdded));
        //}

    }
}
