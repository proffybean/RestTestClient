﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HttpClient.Models
{
    [DataContract]
    public class PlugboardDto
    {
        [DataMember]
        public Dictionary<char, char> Wiring { get; set; }
    }
}