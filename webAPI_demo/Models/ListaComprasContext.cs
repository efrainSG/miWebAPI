using Microsoft.EntityFrameworkCore;

namespace webAPI_demo.Models {
    public class ListaComprasContext:DbContext {
        public ListaComprasContext(DbContextOptions<ListaComprasContext> opciones): base(opciones) {

        }
        public DbSet<ItemCompra> LIstaComprasItems { get; set; }
    }
}
