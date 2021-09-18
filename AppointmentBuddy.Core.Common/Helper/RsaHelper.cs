using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentBuddy.Core.Common.Helper
{
    public class RsaHelper
    {
        public static RSA PrivateKeyFromPemFile(Stream stream)
        {
            string line = string.Empty;
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(stream))
            {
                int intC;
                while ((intC = sr.Read()) != -1)
                {
                    char c = (char)intC;
                    if (c == '\n')
                    {
                        sb.Append(Environment.NewLine);
                    }
                    if (sb.Length >= 2000)
                    {
                        throw new Exception("input too long");
                    }
                    sb.Append(c);
                }

                line = sb.ToString();
            }

            using (TextReader privateKeyTextReader = new StringReader(line))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();


                RsaPrivateCrtKeyParameters privateKeyParams = ((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                var cryptoServiceProvider = RSA.Create();
                RSAParameters parms = new RSAParameters();

                parms.Modulus = privateKeyParams.Modulus.ToByteArrayUnsigned();
                parms.P = privateKeyParams.P.ToByteArrayUnsigned();
                parms.Q = privateKeyParams.Q.ToByteArrayUnsigned();
                parms.DP = privateKeyParams.DP.ToByteArrayUnsigned();
                parms.DQ = privateKeyParams.DQ.ToByteArrayUnsigned();
                parms.InverseQ = privateKeyParams.QInv.ToByteArrayUnsigned();
                parms.D = privateKeyParams.Exponent.ToByteArrayUnsigned();
                parms.Exponent = privateKeyParams.PublicExponent.ToByteArrayUnsigned();

                cryptoServiceProvider.ImportParameters(parms);

                return cryptoServiceProvider;
            }
        }
    }
}
