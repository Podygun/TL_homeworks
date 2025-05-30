﻿using Domain.Entities;

namespace PropertiesApi.Dtos.Properties;

/// <summary>
/// Для передачи в get запросы
/// </summary>
public class PropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}