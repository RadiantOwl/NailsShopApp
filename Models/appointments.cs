//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NailsShopApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class appointments
    {
        public int id_appointment { get; set; }
        public int Id_customer { get; set; }
        public int id_procedure { get; set; }
    
        public virtual customers customers { get; set; }
        public virtual procedures procedures { get; set; }
    }
}
