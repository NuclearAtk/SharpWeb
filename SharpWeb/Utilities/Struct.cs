﻿using System;
using System.Runtime.InteropServices;
using static SharpWeb.Utilities.Natives;
using static SharpWeb.Utilities.Enums;

namespace SharpWeb.Utilities
{
    class Struct
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct BCRYPT_PSS_PADDING_INFO
        {
            public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
            {
                this.pszAlgId = pszAlgId;
                this.cbSalt = cbSalt;
            }

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszAlgId;
            public int cbSalt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
        {
            public int cbSize;
            public int dwInfoVersion;
            public IntPtr pbNonce;
            public int cbNonce;
            public IntPtr pbAuthData;
            public int cbAuthData;
            public IntPtr pbTag;
            public int cbTag;
            public IntPtr pbMacContext;
            public int cbMacContext;
            public int cbAAD;
            public long cbData;
            public int dwFlags;

            public BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(byte[] iv, byte[] aad, byte[] tag) : this()
            {
                dwInfoVersion = BCRYPT_INIT_AUTH_MODE_INFO_VERSION;
                cbSize = Marshal.SizeOf(typeof(BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));

                if (iv != null)
                {
                    cbNonce = iv.Length;
                    pbNonce = Marshal.AllocHGlobal(cbNonce);
                    Marshal.Copy(iv, 0, pbNonce, cbNonce);
                }

                if (aad != null)
                {
                    cbAuthData = aad.Length;
                    pbAuthData = Marshal.AllocHGlobal(cbAuthData);
                    Marshal.Copy(aad, 0, pbAuthData, cbAuthData);
                }

                if (tag != null)
                {
                    cbTag = tag.Length;
                    pbTag = Marshal.AllocHGlobal(cbTag);
                    Marshal.Copy(tag, 0, pbTag, cbTag);

                    cbMacContext = tag.Length;
                    pbMacContext = Marshal.AllocHGlobal(cbMacContext);
                }
            }

            public void Dispose()
            {
                if (pbNonce != IntPtr.Zero) Marshal.FreeHGlobal(pbNonce);
                if (pbTag != IntPtr.Zero) Marshal.FreeHGlobal(pbTag);
                if (pbAuthData != IntPtr.Zero) Marshal.FreeHGlobal(pbAuthData);
                if (pbMacContext != IntPtr.Zero) Marshal.FreeHGlobal(pbMacContext);
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct BCRYPT_OAEP_PADDING_INFO
        {
            public BCRYPT_OAEP_PADDING_INFO(string alg)
            {
                pszAlgId = alg;
                pbLabel = IntPtr.Zero;
                cbLabel = 0;
            }

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszAlgId;
            public IntPtr pbLabel;
            public int cbLabel;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VAULT_ITEM_WIN8
        {
            public Guid SchemaId;
            public IntPtr pszCredentialFriendlyName;
            public IntPtr pResourceElement;
            public IntPtr pIdentityElement;
            public IntPtr pAuthenticatorElement;
            public IntPtr pPackageSid;
            public UInt64 LastModified;
            public UInt32 dwFlags;
            public UInt32 dwPropertiesCount;
            public IntPtr pPropertyElements;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VAULT_ITEM_WIN7
        {
            public Guid SchemaId;
            public IntPtr pszCredentialFriendlyName;
            public IntPtr pResourceElement;
            public IntPtr pIdentityElement;
            public IntPtr pAuthenticatorElement;
            public UInt64 LastModified;
            public UInt32 dwFlags;
            public UInt32 dwPropertiesCount;
            public IntPtr pPropertyElements;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        public struct VAULT_ITEM_ELEMENT
        {
            [FieldOffset(0)] public VAULT_SCHEMA_ELEMENT_ID SchemaElementId;
            [FieldOffset(8)] public VAULT_ELEMENT_TYPE Type;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_HANDLE_INFORMATION
        {
            public ushort ProcessID;
            public ushort CreatorBackTrackIndex;
            public byte ObjectType;
            public byte HandleAttribute;
            public ushort Handle;
            public IntPtr Object_Pointer;
            public IntPtr AccessMask;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct IO_STATUS_BLOCK
        {
            public uint Status;
            public IntPtr Information;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FileNameInformation
        {
            public int NameLength;
            public char Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GENERIC_MAPPING
        {
            public int GenericRead;
            public int GenericWrite;
            public int GenericExecute;
            public int GenericAll;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;

            public override string ToString()
            {
                return Buffer != IntPtr.Zero ? Marshal.PtrToStringUni(Buffer, Length / 2) : null;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_TYPE_INFORMATION
        {
            public UNICODE_STRING Name;
            int TotalNumberOfObjects;
            int TotalNumberOfHandles;
            int TotalPagedPoolUsage;
            int TotalNonPagedPoolUsage;
            int TotalNamePoolUsage;
            int TotalHandleTableUsage;
            int HighWaterNumberOfObjects;
            int HighWaterNumberOfHandles;
            int HighWaterPagedPoolUsage;
            int HighWaterNonPagedPoolUsage;
            int HighWaterNamePoolUsage;
            int HighWaterHandleTableUsage;
            int InvalidAttributes;
            GENERIC_MAPPING GenericMapping;
            int ValidAccess;
            bool SecurityRequired;
            bool MaintainHandleCount;
            ushort MaintainTypeList;
            POOL_TYPE PoolType;
            int PagedPoolUsage;
            int NonPagedPoolUsage;
        }
    }
}
