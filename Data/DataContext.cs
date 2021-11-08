using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enums;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        //Digite ctor + TAB  - atalho para criar a estrutura básica de um construtor.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
          
        }
        /// Digite Prop + TAB
        public DbSet<Personagem> Personagens { get; set; } 
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<PersonagemHabilidade> PersonagemHabilidades { get; set; }     
        public DbSet<Disputa> Disputas { get; set; }   

        //OnModelCreating = DURANTE A CRIÇÃO DA TABELA

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().HasData
            (
                new Personagem() { Id = 1 }, //Frodo Cavaleiro             
                new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},     
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
                new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }      
            );

            modelBuilder.Entity<Arma>().HasData
            (
                new Arma() { Id = 1, Nome = "Espada", Dano=25, PersonagemId=1},              
                new Arma() { Id = 2, Nome = "Machado", Dano=30, PersonagemId=2 },     
                new Arma() { Id = 3, Nome = "Crucifixo", Dano=20, PersonagemId=3 }    
            );

            modelBuilder.Entity<PersonagemHabilidade>().HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId});

              modelBuilder.Entity<Habilidade>().HasData
            (
                new Habilidade() { Id = 1, Nome = "Bola de fogo", Dano=29},              
                new Habilidade() { Id = 2, Nome = "Portal", Dano=41 },     
                new Habilidade() { Id = 3, Nome = "Congelar", Dano=37 }    
            );

            modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");
        }
    }
}