using System;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;

namespace GP2F.Model
{
    public class Salon
    {
        public virtual int SalonId { get; set; }
        public virtual string SalonAd { get; set; }
        public virtual int Kapasite { get; set; }
    }
    public class SalonMap : ClassMapping<Salon>
    {
        public SalonMap()
        {
            Id(x => x.SalonId, m => m.Generator(Generators.Native));
            Property(x => x.SalonId);
            Property(x => x.Kapasite);

        }
    }
}
