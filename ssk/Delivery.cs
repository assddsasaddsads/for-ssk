//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ssk
{
    using System;
    using System.Collections.Generic;
    
    public partial class Delivery
    {
        public int Id { get; set; }
        public int Id_Orders { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
    
        public virtual Orders Orders { get; set; }
    }
}
