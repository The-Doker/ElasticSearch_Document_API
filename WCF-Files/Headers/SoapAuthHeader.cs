using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WCF_Files.Models;

namespace WCF_Files.Headers
{
    public class SoapAuthHeader : MessageHeader
    {
        private const string CUSTOM_HEADER_NAME = "AuthDataHeader";
        private const string CUSTOM_HEADER_NAMESPACE = "AuthDataNamespace";

        private AuthDataModel _customData;

        public AuthDataModel CustomData
        {
            get
            {
                return _customData;
            }
        }

        public SoapAuthHeader(AuthDataModel customData)
        {
            _customData = customData;
        }

        public override string Name
        {
            get { return (CUSTOM_HEADER_NAME); }
        }

        public override string Namespace
        {
            get { return (CUSTOM_HEADER_NAMESPACE); }
        }

        protected override void OnWriteHeaderContents(
            XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AuthDataModel));
            StringWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, _customData);
            textWriter.Close();

            string text = textWriter.ToString();

            writer.WriteElementString(CUSTOM_HEADER_NAME, "Key", text.Trim());
        }

        public static AuthDataModel ReadHeader(Message request)
        {
            Int32 headerPosition = request.Headers.FindHeader(CUSTOM_HEADER_NAME, CUSTOM_HEADER_NAMESPACE);
            if (headerPosition == -1)
                return null;

            MessageHeaderInfo headerInfo = request.Headers[headerPosition];

            XmlNode[] content = request.Headers.GetHeader<XmlNode[]>(headerPosition);

            string text = content[0].InnerText;

            XmlSerializer deserializer = new XmlSerializer(typeof(AuthDataModel));
            TextReader textReader = new StringReader(text);
            AuthDataModel customData = (AuthDataModel)deserializer.Deserialize(textReader);
            textReader.Close();

            return customData;
        }
    }
}