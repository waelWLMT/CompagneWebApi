using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignPubTest.MockData.DataMock
{
    public static class ProductTypeMockData
    {
        public static List<ProductType> GetAllProductTypes()
        {
            var productTypes = new List<ProductType>();
            productTypes.Add(new ProductType()
            {
                Name = "Flyer",
                Price = 3.5F,
                Size = new Core.CompelxeTypes.Size
                    {
                        Height = 15.5F,
                        Width = 12.5F,
                        Unit = Core.Enums.DimensionUnit.cm
                    },
                Color = "#000000",
                Description = "Flyer de publicité",
                Id = 1
            });
            productTypes.Add(new ProductType()
            {
                Name = "Panneau Pub",
                Price = 2.5F,
                Size =new Core.CompelxeTypes.Size()
                    {
                        Height = 15.5F,
                        Width = 12.5F,
                        Unit = Core.Enums.DimensionUnit.cm
                    },
                Color = "#000000",
                Description = "Panneau de publcitéé",
                Id = 2
            });
            productTypes.Add(new ProductType()
                {
                    Name = "Sachert à baguette",
                    Price = 0.2F,
                    Size = null,
                    Color = "#000000",
                    Description = "Sachet à baguette pour boulangerie épicerie  etc ...",
                    Id = 3
                });

            

            return productTypes;
            
        }

        public static List<ProductType> GetAllProductTypesInListIds(List<int> ids)
        {
            return GetAllProductTypes().FindAll(x => ids.Contains(x.Id));
        }
    }
}

