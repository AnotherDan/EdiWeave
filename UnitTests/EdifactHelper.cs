﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using EdiFabric.Core.Model;
using EdiFabric.Core.Model.Edi;
using EdiFabric.Core.Model.Edi.Edifact;
using EdiFabric.Framework;
using EdiFabric.Framework.Writers;
using EdiFabric.Rules.EDIFACT_D00A;

namespace EdiFabric.UnitTests
{
    public class EdifactHelper
    {
        public static string Generate(List<object> items, Separators separators, string postFix,
            Encoding encoding = null, string una = null)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new EdifactWriter(stream, encoding, postFix);
                if (!string.IsNullOrEmpty(una))
                    writer.WriteSegment(una);
                foreach (var item in items)
                {
                    var message = item as EdiMessage;
                    if (message != null)
                    {
                        writer.WriteMessage(message);
                        continue;
                    }

                    var gs = item as UNG;
                    if (gs != null)
                    {
                        writer.WriteGroup(gs);
                        continue;
                    }

                    var ge = item as UNE;
                    if (ge != null)
                    {
                        continue;
                    }

                    var unb = item as UNB;
                    if (unb != null)
                    {
                        writer.WriteInterchange(unb, separators);
                    }
                }
                writer.Flush();

                return CommonHelper.LoadString(stream);
            }
        }

        public static UNB CreateUnb()
        {
            return new UNB
            {
                SYNTAXIDENTIFIER_1 = new S001
                {
                    // Syntax Identifier
                    SyntaxIdentifier_1 = "UNOB",
                    // Syntax Version Number
                    SyntaxVersionNumber_2 = "1"
                },
                INTERCHANGESENDER_2 = new S002
                {
                    // Interchange sender identification
                    InterchangeSenderIdentification_1 = "SENDERID",
                    // Identification code qualifier
                    IdentificationCodeQualifier_2 = "01",
                    // Interchange sender internal identification
                    InterchangeSenderInternalIdentification_3 = "ZZUK"
                },
                INTERCHANGERECIPIENT_3 = new S003
                {
                    // Interchange recipient identification
                    InterchangeRecipientIdentification_1 = "RECEIVERID",
                    // Identification code qualifier
                    IdentificationCodeQualifier_2 = "02",
                    // Interchange recipient internal identification
                    InterchangeRecipientInternalIdentification_3 = "ZZUK"
                },
                DATEANDTIMEOFPREPARATION_4 = new S004
                {
                    // Date
                    Date_1 = "170509",
                    // Time
                    Time_2 = "1241"
                },
                // Interchange control reference
                // Must be incremented with every interchange
                InterchangeControlReference_5 = "1",
                // Application reference
                ApplicationReference_7 = "INVOIC"
            };
        }

        public static TSINVOIC CreateInvoice()
        {
            var result = new TSINVOIC
            {
                UNH = new UNH
                {
                    MessageReferenceNumber_01 = "123",
                    MessageIdentifier_02 = new S009
                    {
                        MessageTypeIdentifier_01 = "INVOIC",
                        MessageTypeVersionNumber_02 = "D00A",
                        MessageTypeReleaseNumber_03 = "UN"
                    }
                },
                BGM = new BGM
                {
                    DOCUMENTMESSAGENAME_01 = new C002 {Documentnamecode_01 = "123"},
                    DOCUMENTMESSAGEIDENTIFICATION_02 = new C106
                    {
                        Documentidentifier_01 = "12345678",
                        Versionidentifier_02 = "9"
                    },
                    Messagefunctioncode_03 = "AA"
                },
                DTM = new List<DTM>(),
                DGS = new DGS
                {
                    Dangerousgoodsregulationscode_01 = "",
                    HAZARDCODE_02 = new C205
                    {
                        Hazardidentificationcode_01 = "",
                        Additionalhazardclassificationidentifier_02 = ""
                    }
                }
            };

            var dtm = new DTM
            {
                DATETIMEPERIOD_01 = new C507
                {
                    Dateortimeorperiodfunctioncodequalifier_01 = "137",
                    Dateortimeorperiodvalue_02 = "19980610",
                    Dateortimeorperiodformatcode_03 = "102"
                }
            };
            result.DTM.Add(dtm);

            return result;
        }
    }
}