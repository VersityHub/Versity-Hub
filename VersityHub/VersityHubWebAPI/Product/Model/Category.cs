using System;
using System.ComponentModel.DataAnnotations;

namespace VersityHub.VersityHubWebAPI.Product.Model
{
    public class Category 
    {
       /* public Category() { }

        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Code { get; private set; } = default!;
        public (CategoryId id, string) Id { get; private set; }

        public static Category Create(CategoryId id, string name, string code, string description = "")
        {
            var category = new Category { Id = (id, nameof(id)) };

            category.ChangeName(name);
            category.ChangeDescription(description);
            category.ChangeCode(code);

            return category;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ApplicationException("Name can't be white space or null.");

            Name = name;
        }

        public void ChangeCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ApplicationException("Code can't be white space or null.");

            Code = code;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ApplicationException("Description can't be white space or null.");

            Description = description;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }*/
    }
}
