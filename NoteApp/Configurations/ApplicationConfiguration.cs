using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Data;
using NoteApp.Entities;

namespace NoteApp.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable(nameof(Note));
            builder.HasData(
                new Note
                {
                    Id = Guid.NewGuid().ToString(),
                    Title= "The Art of War",
                    Body= "Widely regarded as \"The Oldest Military Treatise in the World,\" this landmark work covers principles of strategy, tactics, maneuvering, communication, and supplies; the use of terrain, fire, and the seasons of the year; the classification ...",
                    CreatedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                }
                );
        }
    }
}
