﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WKFramework.Utils.Serializer
{
    public class BinarySerializer : Serializer<byte[]>
    {
        protected override byte[] DoSerialize<TSource>(TSource obj)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);

                var bytes = new byte[stream.Length];
                int read = stream.Read(bytes, 0, (int)stream.Length);

                if (read != stream.Length)
                    throw new InvalidOperationException("Couldn't serialize whole object.");

                return bytes;
            }
        }

        protected override TResult DoDeserialize<TResult>(byte[] data)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);

                return (TResult)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
