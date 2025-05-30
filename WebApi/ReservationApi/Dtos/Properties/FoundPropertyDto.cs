﻿using ReservationApi.Dtos.RoomTypes;

namespace ReservationApi.Dtos.Properties;

public class FoundPropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public IList<RoomTypeDto> RoomTypes { get; set; } = [];
}
