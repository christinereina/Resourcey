using System;
using System.Collections.Generic;
using Resourcey.Enums;
using System.ComponentModel.DataAnnotations;

namespace Resourcey.Models
{
  public class Resource
  {
    [Required]
    public string Name { get; set; }
    public int ResourceId { get; set; }
    [Display(Name = "Date Added")]
    [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:d}")]
    public DateTime Date { get; set; }
    [Required]
    public string ResourceUrl { get; set; }
    public ResourceType Type { get; set; }
    public string Description { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public virtual ApplicationUser ResourceCreator {get; set;}
  }
}