using Microsoft.EntityFrameworkCore;
using MusicaComEF.API.Models;

namespace MusicaComEF.API.Data
{
    public class MusicasDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MusicasDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<ArtistaModel> Artistas { get; set; }

        public DbSet<AlbumModel> Albuns { get; set; }

        public DbSet<MusicaModel> Musicas { get; set; }

        public DbSet<PlayListModel> PlayLists { get; set; }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DB_MUSICAS"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistaModel>(entidade =>
            {
                // NOME DA TABELA
                entidade.ToTable("Artistas");

                // CHAVE PRIMARIA
                entidade.HasKey(classeArtista => classeArtista.Id);


                entidade
                .Property(classeArtista => classeArtista.Nome) // COLUNA, NOME DA PROPRIEDADE
                .HasMaxLength(120)                             // QUANTIDADE PERMITIDA DE CARACTERES
                .IsRequired();                                 // O CAMPO É REQUERIDO


                entidade
                .Property(classeArtista => classeArtista.NomeArtistico) // COLUNA, NOME DA PROPRIEDADE
                .HasMaxLength(120);                                   // QUANTIDADE PERMITIDA DE CARACTERES

                entidade
                .Property(classeArtista => classeArtista.PaisOrigem) // COLUNA, NOME DA PROPRIEDADE
                .HasMaxLength(80);                                   // QUANTIDADE PERMITIDA DE CARACTERES

                entidade
                .Property(classeArtista => classeArtista.Idade); // COLUNA, NOME DA PROPRIEDADE

                entidade
                // SERVE PARA INSERIR DADOS DENTRO DA TABELA
                .HasData(new[] {
                  new ArtistaModel{
                  Id = 1,
                  Nome= "Daniel",
                  NomeArtistico= "Daniel",
                  PaisOrigem = "Brasil",
                  Idade = 49
                  },
                   new ArtistaModel{
                  Id = 2,
                  Nome= "Anitta",
                  NomeArtistico= "Anitta",
                  PaisOrigem = "Brasil",
                  Idade = 26
                  }
                });



            });

            modelBuilder.Entity<AlbumModel>(entidade =>
            {
                entidade.ToTable("Albuns");

                entidade.HasKey(classeAlbum => classeAlbum.Id);

                entidade
               .Property(classeAlbum => classeAlbum.Nome)
               .HasMaxLength(120)
               .IsRequired();

                entidade
                .Property(classeAlbum => classeAlbum.AnoLancamento)
                .IsRequired();

                entidade
                .Property(classeAlbum => classeAlbum.CapaUrl)
                .HasMaxLength(100);

                entidade
                //  CADA ALBUM CONTEM UM ARTISTA
                .HasOne(classeAlbum => classeAlbum.Artista)
                //  CADA ARTISTA CONTEM VARIOS ALBUNS
                .WithMany(artista => artista.Albuns)
                //  CHAVE ESTRANGEIRA
                .HasForeignKey(classeAlbum => classeAlbum.ArtistaId);
               
            });

            modelBuilder.Entity<MusicaModel>(entidade =>
            {
                entidade.ToTable("Musicas");

                entidade.HasKey(classeMusica => classeMusica.Id);

                entidade
               .Property(classeMusica => classeMusica.Nome)
               .HasMaxLength(120)
               .IsRequired();

                entidade
                .Property(classeMusica => classeMusica.Duracao);


                entidade
              //  UMA MUSICA CONTEM UM ALBUM
              .HasOne(classeMusica => classeMusica.Album)
              //  CADA ALBUM CONTEM VARIAS MUSICAS
              .WithMany(album => album.Musicas)
              //  CHAVE ESTRANGEIRA
              .HasForeignKey(classeMusica => classeMusica.AlbumId);

                entidade
            //  UMA MUSICA CONTEM UM ARTISTA
            .HasOne(classeMusica => classeMusica.Artista)
            //  CADA ARTISTA CONTEM VARIAS MUSICAS
            .WithMany(artista => artista.Musicas)
            //  CHAVE ESTRANGEIRA
            .HasForeignKey(classeMusica => classeMusica.ArtistaId);


                entidade
            //  UMA MUSICA CONTEM UMA PLAYLIST
            .HasOne(classeMusica => classeMusica.PlayList)
            //  CADA PLAYLIST CONTEM VARIAS MUSICAS
            .WithMany(playList => playList.Musicas)
            //  CHAVE ESTRANGEIRA
            .HasForeignKey(classeMusica => classeMusica.PlayListId);
            });

            modelBuilder.Entity<PlayListModel>(entidade =>
            {
            // NOME DA TABELA
            entidade.ToTable("PlayLists");

            // CHAVE PRIMARIA
            entidade.HasKey(classePlayList => classePlayList.Id);

            entidade
            .Property(classePlayList => classePlayList.Nome)
            .HasMaxLength(120)
            .IsRequired();

            entidade
            .Property(classePlayList => classePlayList.Genero)
            .HasMaxLength(50);

            });
            

        }
    }
}
