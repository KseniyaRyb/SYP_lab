using System;
using Microsoft.EntityFrameworkCore;

namespace LR1
{
    public class DatabaseContext : DbContext //Наследуемся от DBContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) // Не забываем звать основной конструктор 
        {
        }

        public DbSet<CommandEntity> CommandEntities { get; set; } //Объявляем таблицу для сущности
    }
}

