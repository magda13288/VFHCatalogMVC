using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using Xunit;

namespace Application.UnitTests.Validations
{
    public class AddNewPlantValidationTests
    {
        [Fact]

        public void Add_NewPlant_ProperRequest_ShouldNotReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();

            validator.TestValidate(newPlant).ShouldNotHaveAnyValidationErrors();
            
        }

        [Fact]

        public void Add_NewPlant_InvalidReqest_WrongId_ShouldReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();
            newPlant.Id = -1;

            validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.Id));
        }

        [Fact]

        public void Add_NewPlant_InvalidReqest_WrongTypeId_ShouldReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();
            newPlant.TypeId = 0;

            validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.TypeId));
        }

        [Fact]

        public void Add_NewPlant_InvalidReqest_WrongGroupId_ShouldReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();
            newPlant.GroupId = 0;

            validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.GroupId));
        }

        //[Fact]

        //public void Add_NewPlant_InvalidReqest_WrongSectionId_ShouldReturnValidationError()
        //{
        //    var validator = new NewPlantVm.NewPlantValidation();
        //    var newPlant = SetNewPlantParameters();
        //    newPlant.SectionId = 0;

        //    validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.SectionId));
        //}

        [Fact]

        public void Add_NewPlant_InvalidReqest_WrongFullName_ShouldReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();
            newPlant.FullName = string.Empty;

            validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.FullName));
        }

        [Fact]

        public void Add_NewPlant_InvalidReqest_WrongMaximumLenghtForFullName_ShouldReturnValidationError()
        {
            var validator = new NewPlantVm.NewPlantValidation();
            var newPlant = SetNewPlantParameters();
            newPlant.FullName = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest";

            validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.FullName));
        }

        //[Fact]
        //public void Add_NewPlant_InvalidReqest_WrongPhotoFileName_ShouldReturnValidationError()
        //{
        //    var validator = new NewPlantVm.NewPlantValidation();
        //    var newPlant = SetNewPlantParameters();
        //    newPlant.PhotoFileName = string.Empty;

        //    validator.TestValidate(newPlant).ShouldHaveValidationErrorFor(nameof(newPlant.PhotoFileName));
        //}
        private static NewPlantVm SetNewPlantParameters()
        {
            var plant = new NewPlantVm()
            {
                Id = 1,
                TypeId = 1,
                GroupId = 1,
                SectionId = 1,
                FullName = "Test",
                PhotoFileName = "Test",
                PlantDetails = new PlantDetailsVm()
                {
                    ColorId = 1,
                    FruitSizeId = 1,
                    FruitTypeId = 1,
                    Description = "Test",
                    ListGrowingSeazons = new ListGrowingSeazonsVm() { GrowingSeaznosIds = new int[] { 1, 2 } },
                    ListGrowthTypes = new ListGrowthTypesVm() { GrowthTypesIds = new int[] { 1 } },
                    ListPlantDestinations = new ListPlantDestinationsVm() { DestinationsIds = new int[] { 1, 2 } },
                }

            };

            return plant;

        }
    }
}
