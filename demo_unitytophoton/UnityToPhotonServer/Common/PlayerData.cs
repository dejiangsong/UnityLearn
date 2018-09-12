using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    [Serializable]
    public class PlayerData {
        public string Username { get; set; }
        public Vector3Data Position { get; set; }

    }

    [Serializable]
    public class Vector3Data {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
