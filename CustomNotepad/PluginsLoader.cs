using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using PluginsContracts;

namespace CustomNotepad
{
    public class PluginsLoader<T>
    {
        private CompositionContainer _container;

        [ImportMany]
        public IEnumerable<T> Plugins { get; set; }

        /*[Import(typeof(IPlugin))]
        public IPlugin plugin;*/

        public PluginsLoader(string path)
        {
            DirectoryCatalog dir = new DirectoryCatalog(path);
            var catalog = new AggregateCatalog(dir);
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
        }
    }
}
