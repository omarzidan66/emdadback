using System;
using System.Collections.Generic;

namespace Emdad.Models;

public  class FormField : BaseEntity
{
    public int FormFieldId { get; set; }

    public int SectorsServicesId { get; set; }

    public string FormFieldLabel { get; set; }

    public string FormFieldName { get; set; }

    public string FormFieldType { get; set; }

    public bool FormFieldIsRequired { get; set; }
    public SectorsServices SectorsServices { get; set; }
}
