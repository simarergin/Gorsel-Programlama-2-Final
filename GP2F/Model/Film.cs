using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP2F.Model
{
    public class Film
    {
        public virtual int FilmId { get; set; }
        public virtual string FilmAd { get; set; }
        public virtual int FilmTarih { get; set; }
        public virtual int SeansSaat { get; set; }
    }

    public class FilmMap : ClassMapping<Film>
    {
        public FilmMap()
        {
            Id(x => x.FilmId, m => m.Generator(Generators.Native));
            Property(x => x.FilmAd);
            Property(x => x.FilmTarih);
            Property(x => x.SeansSaat);
        }
    }
}
