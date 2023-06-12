using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP2F.Model
{
    public class Seans
    {
        public virtual int SeansId { get; set; }
        public virtual string AdSoyad { get; set; }
        public virtual int BiletSayi { get; set; }
        public virtual string BiletAraligi { get; set; }
    }
    public class SeansMap : ClassMapping<Seans>
    {
        public SeansMap()
        {
            Id(x => x.SeansId, m => m.Generator(Generators.Native));
            Property(x => x.AdSoyad);
            Property(x => x.BiletSayi);
            Property(x => x.BiletAraligi);
        }
    }
}
