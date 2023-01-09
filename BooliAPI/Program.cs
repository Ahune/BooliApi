using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PropertiesSoldDb>(opt => opt.UseInMemoryDatabase("PropertiesSoldList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World Let calculate some prices in the real estate market! Start by using Boolis API to return a JSON of sold element.");

app.MapGet("/mylist", async (PropertiesSoldDb db) =>
    await db.SoldProperties.ToListAsync());

app.MapGet("/mylist/{id}", async (int id, PropertiesSoldDb db) =>
    await db.SoldProperties.FindAsync(id)
        is SoldProperty sell
            ? Results.Ok(sell)
            : Results.NotFound());

app.MapGet("/avgprice", async (PropertiesSoldDb db) =>
{
    var objects = await db.SoldProperties.ToListAsync();

    var avgPricePerSquareMeter = CalcAvgPrice(objects);
    return Results.Ok(avgPricePerSquareMeter);
});

app.MapPost("/solditems", async (SoldProperty sells, PropertiesSoldDb db) =>
{
    if (sells is null)
    {
        return Results.NotFound();
    }

    db.SoldProperties.Add(sells);
    await db.SaveChangesAsync();

    return Results.Created($"/Solditems/{sells.Id}", sells);

});

app.MapPost("/multipleAdd", async (List<SoldProperty> sells, PropertiesSoldDb db) =>
{
    if (sells is null || sells.Count <= 0)
    {
        return Results.NotFound();
    }

    foreach (var item in sells)
    {
        db.SoldProperties.Add(item);

    }
    await db.SaveChangesAsync();

    return Results.Created($"/Solditems/{sells.Count}", sells);

});

app.MapPut("/Solditems/{id}", async (int id, SoldProperty inputSold, PropertiesSoldDb db) =>
{
    var sold = await db.SoldProperties.FindAsync(id);

    if (sold is null) return Results.NotFound();

    sold.Rooms = inputSold.Rooms;
    sold.ConstructionYear = inputSold.ConstructionYear;
    sold.SoldDate = inputSold.SoldDate;
    sold.AdditionalArea = inputSold.AdditionalArea;
    sold.Rent = inputSold.Rent;
    sold.LivingArea = inputSold.LivingArea;
    sold.SoldPriceSource = inputSold.SoldPriceSource;
    sold.ListPrice = inputSold.ListPrice;
    sold.SoldPrice = inputSold.SoldPrice;
    sold.BooliId = inputSold.BooliId;
    sold.Floor = inputSold.Floor;
    sold.ObjectType = inputSold.ObjectType;
    sold.Published = inputSold.Published;
    sold.ApartmentNumber = inputSold.ApartmentNumber;
    sold.Url = inputSold.Url;

    await db.SaveChangesAsync();
    return Results.NoContent();

});

app.MapDelete("/solditems/{id}", async (int id, PropertiesSoldDb db) =>
{
    if (await db.SoldProperties.FindAsync(id) is SoldProperty soldItem)
    {
        db.Remove(soldItem);
        await db.SaveChangesAsync();
        return Results.Ok(soldItem);
    }
    return Results.NotFound();
});

app.Run();


//Better on the client side
float CalcAvgPrice(List<SoldProperty> objects)
{
    float avgPricePerSquareMeter = 0f;
    float sqrmeter = 0f;
    float sellingPrice = 0f;
    float pricePerSqr = 0f;
    int numberOfObjects = objects.Count;
    List<float> pricePerSqrList = new List<float>();

    foreach (var item in objects)
    {
        sellingPrice = (float)item.SoldPrice;
        sqrmeter = (float)item.LivingArea;
        pricePerSqr = sellingPrice / sqrmeter;
        pricePerSqrList.Add(pricePerSqr);
    }

    avgPricePerSquareMeter = (pricePerSqrList.Sum() / pricePerSqrList.Count);


    return MathF.Round(avgPricePerSquareMeter);
} 

class PropertiesSoldDb : DbContext
{
    public PropertiesSoldDb(DbContextOptions<PropertiesSoldDb> options)
        : base(options) { }

    public DbSet<SoldProperty> SoldProperties => Set<SoldProperty>();
}

class SoldProperty
{
    public int Id { get; set; }
    public int Rooms { get; set; }
    public int ConstructionYear { get; set; }
    public string? SoldDate { get; set; }
    public int AdditionalArea { get; set; }
    public int Rent { get; set; }
    public int LivingArea { get; set; }
    public string? SoldPriceSource { get; set; }
    public float ListPrice { get; set; }
    public int SoldPrice { get; set; }
    public int BooliId { get; set; }
    public int Floor { get; set; }
    public string? ObjectType { get; set; }
    public string? Published { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? Url { get; set; }
}
