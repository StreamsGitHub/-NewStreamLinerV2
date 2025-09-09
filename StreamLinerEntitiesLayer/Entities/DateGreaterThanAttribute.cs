using System;
using System.ComponentModel.DataAnnotations;

namespace StreamLinerEntitiesLayer.Entities;
public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _startDatePropertyName;

    public DateGreaterThanAttribute(string startDatePropertyName)
    {
        _startDatePropertyName = startDatePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
        if (startDateProperty == null)
        {
            return new ValidationResult($"Unknown property: {_startDatePropertyName}");
        }

        var startDateValue = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);
        var dueDateValue = (DateTime?)value;

        if (dueDateValue.HasValue && startDateValue.HasValue && dueDateValue.Value <= startDateValue.Value)
        {
            return new ValidationResult("DueDate must be after StartDate.");
        }

        return ValidationResult.Success;
    }
}

