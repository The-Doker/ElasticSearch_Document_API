using ElasticSearch_Document_API.Models;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;

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
}