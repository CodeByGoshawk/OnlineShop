﻿using PublicTools.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
public class ChangePasswordAppDto
{
    [RequesterId]
    public string? UserId { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Required]
    public string CurrentPassword { get; set; }
}
