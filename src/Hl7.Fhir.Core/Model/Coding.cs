﻿using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Text;

/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

//
// Generated on Wed, Dec 24, 2014 16:02+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public partial class Coding : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        internal string DebuggerDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(this.Code))
                    sb.AppendFormat(" Code=\"{0}\"", Code);
                if (!string.IsNullOrEmpty(this.Display))
                    sb.AppendFormat(" Display=\"{0}\"", Display);
                if (!string.IsNullOrEmpty(this.System))
                    sb.AppendFormat(" System=\"{0}\"", System);

                return sb.ToString();
            }
        }
    }
}