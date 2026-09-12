using EventsHub.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventsHub.Persistence;

public static class DbInitializer
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
        if (await context.Events.AnyAsync()) return;

        var locations = new (string City, string Venue, string Latitude, string Longitude)[]
        {
            ("Tinúm, Yucatán", "Chichén Itzá", "20.6843", "-88.5678"),
            ("San Juan Teotihuacán, Estado de México", "Pirámide del Sol, Teotihuacán", "19.6925", "-98.8438"),
            ("Ciudad de México", "Zócalo (Plaza de la Constitución)", "19.4326", "-99.1332"),
            ("Ciudad de México", "Palacio de Bellas Artes", "19.4352", "-99.1412"),
            ("Cancún, Quintana Roo", "Playa Delfines", "21.0997", "-86.7561"),
            ("Guanajuato, Guanajuato", "Callejón del Beso", "21.0190", "-101.2574"),
            ("Ciudad de México", "Xochimilco (Trajineras)", "19.2828", "-99.1036"),
            ("Ciudad de México", "Basílica de Guadalupe", "19.4847", "-99.1176"),
            ("Tulum, Quintana Roo", "Zona Arqueológica de Tulum", "20.2114", "-87.4287"),
            ("Guadalajara, Jalisco", "Palacio de Gobierno de Jalisco", "20.6767", "-103.3475"),
            ("Aguascalientes, Aguascalientes", "Jardín de San Marcos", "21.8798", "-102.3023"),
            ("Puebla, Puebla", "Catedral de Puebla", "19.0433", "-98.1980"),
            ("Monterrey, Nuevo León", "Parque Fundidora", "25.6780", "-100.2850"),
            ("Mérida, Yucatán", "Paseo de Montejo", "20.9870", "-89.6190"),
            ("Querétaro, Querétaro", "Acueducto de Querétaro", "20.5960", "-100.3790")
        };

        var categories = new[] { "culture", "music", "drinks", "food", "sports", "travel" };

        var events = new List<Event>();

        for (var i = 1; i <= 67; i++)
        {
            var isPast = i <= 34;
            var offsetMonths = isPast ? -(35 - i) : (i - 34);
            var loc = locations[(i - 1) % locations.Length];
            var category = categories[(i - 1) % categories.Length];

            events.Add(new Event
            {
                Title = isPast ? $"Past Event {i}" : $"Future Event {i - 34}",
                Date = DateTime.Now.AddMonths(offsetMonths),
                Description = isPast ? $"Event {35 - i} months ago" : $"Event {i - 34} months in future",
                Category = category,
                City = loc.City,
                Venue = loc.Venue,
                Latitude = loc.Latitude,
                Longitude = loc.Longitude,
                IsCancelled = i % 7 == 0
            });
        }

        context.Events.AddRange(events);

        await context.SaveChangesAsync();
    }
}
