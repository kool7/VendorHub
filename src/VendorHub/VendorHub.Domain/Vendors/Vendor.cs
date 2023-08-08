﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VendorHub.Domain.Vendors;

public class Vendor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
