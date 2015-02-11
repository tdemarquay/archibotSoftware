using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archibot_1st_version
{

    class Archibot_manager
    {
        public Array scan_table;
        public Generated_image map;

        public Archibot_manager(int size, string path)
        {

            scan_table = new Array(size,path);

            scan_table.set_array();
            scan_table.print_to_txt();
            
            map = new Generated_image(ref scan_table,size);
            map.generate();
        }

        
    }
}
