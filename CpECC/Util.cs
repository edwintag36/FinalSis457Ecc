﻿using CadECC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CpECC
{
    public class Util
    {
		public static Usuario usuario;
		public static string Encrypt(string Text)
		{
			string Password = "SIS457-1nf0!";
			byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(Text);
		
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
				
			    return Convert.ToBase64String(encryptedData);
		}

		private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
		{
			MemoryStream ms = new MemoryStream();
			Rijndael alg = Rijndael.Create();
			alg.Key = Key;
			alg.IV = IV;
			CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(clearData, 0, clearData.Length);
			cs.Close();
            byte[] encryptedData = ms.ToArray();
			return encryptedData;
		}

	}
}
