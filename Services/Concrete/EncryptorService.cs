using Encryptor_Application.Services.Abstract;
using FluentResults;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Concrete
{
    public class EncryptorService : IEncryptorService
    {
        public IEnumerable<byte> Decrypt(IReadOnlyCollection<int> data)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            foreach (var item in data)
            {
                if (!_decryptionKeys.TryGetValue(item, out var decryptedByte))
                {
                    throw new ArgumentException($"The data is encrypted wrong. The key {item} does not exist.");
                }
                yield return decryptedByte;
            }
        }

        public async Task<IEnumerable<byte>> DecryptAsync(IReadOnlyCollection<int> data)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            return await Task.Run(() =>
            {
                var dataArray = data.ToArray(); // for index access
                var result = new byte[dataArray.Length];
                var error = new ConcurrentQueue<string>();

                Parallel.For(0, dataArray.Length, i =>
                {
                    if (_decryptionKeys.TryGetValue(dataArray[i], out var decryptedByte))
                    {
                        result[i] = decryptedByte;
                    }
                    else
                    {
                        error.Enqueue($"The data is encrypted wrong. The key {dataArray[i]} does not exist.");
                    }
                });

                if (!error.IsEmpty)
                {
                    throw new ArgumentException(string.Join("\n", error));
                }

                return (IEnumerable<byte>)result;
            });
        }


        public IEnumerable<int> Encrypt(IReadOnlyCollection<byte> data)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            foreach (var item in data)
            {
                yield return _encryptionKeys[item];
            }
        }

        public Result<IEnumerable<byte>> TryDecrypt(IReadOnlyCollection<int> data)
        {
            if(data is null)
            {
                return Result.Fail<IEnumerable<byte>>("The data is null!");
            }
            var result = new List<byte>(data.Count);
            foreach (var item in data)
            {
                if (!_decryptionKeys.TryGetValue(item, out var decryptedByte))
                {
                    return Result.Fail<IEnumerable<byte>>($"The data is encrypted wrong. The key {item} does not exist.");
                }
                result.Add(decryptedByte);
            }
            return Result.Ok((IEnumerable<byte>)result);
        }

        public async Task<Result<IEnumerable<byte>>> TryDecryptAsync(IReadOnlyCollection<int> data)
        {
            if (data is null)
            {
                return Result.Fail<IEnumerable<byte>>("The data is null!");
            }

            return await Task.Run(() =>
            {
                var dataArray = data.ToArray();
                var result = new byte[dataArray.Length];
                var error = new ConcurrentQueue<string>();

                Parallel.For(0, dataArray.Length, i =>
                {
                    if (_decryptionKeys.TryGetValue(dataArray[i], out var decryptedByte))
                    {
                        result[i] = decryptedByte;
                    }
                    else
                    {
                        error.Enqueue($"The data is encrypted wrong. The key {dataArray[i]} does not exist.");
                    }
                });

                if (!error.IsEmpty)
                {
                    return Result.Fail<IEnumerable<byte>>(string.Join("\n", error));
                }

                return Result.Ok((IEnumerable<byte>)result);
            });
        }

        public async Task<IEnumerable<int>> EncryptAsync(IReadOnlyCollection<byte> data)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            return await Task.Run(() =>
            {
                var dataArray = data.ToArray(); // Ensure index access
                var result = new int[dataArray.Length];

                Parallel.For(0, dataArray.Length, i =>
                {
                    result[i] = _encryptionKeys[dataArray[i]];
                });

                return (IEnumerable<int>)result;
            });
        }



        private readonly int[] _encryptionKeys = new int[] {
                1421734017,
            829013511,
            2027105908,
            1063693803,
            1195000380,
            1565346800,
            1385181503,
            1122174322,
            2067663856,
            324763066,
            620937206,
            1510248086,
            1922777174,
            1095083620,
            173377474,
            930944486,
            1318955406,
            344200625,
            974076566,
            1400709108,
            1710188955,
            625838511,
            1653037201,
            1785448126,
            1035476092,
            1738731682,
            1878921457,
            252201760,
            1928998208,
            1669753162,
            542500117,
            1624916871,
            1125009911,
            2021386870,
            1031683535,
            1081111110,
            1153801867,
            1708165281,
            1434083041,
            923667551,
            2044875087,
            930919528,
            1320575827,
            927092032,
            40974813,
            780031496,
            1338832053,
            667829553,
            385492148,
            928004841,
            241479314,
            586279124,
            1800721793,
            1026094676,
            82964121,
            1670820305,
            1616511528,
            1412650953,
            1220761641,
            34752446,
            7875838,
            287236086,
            1050574342,
            1962224209,
            1618141950,
            664012968,
            565362535,
            171067959,
            828854482,
            952676850,
            80239269,
            1721265542,
            1032742146,
            857522932,
            574686632,
            1551945540,
            2015881796,
            908377516,
            1958939161,
            783989133,
            728004399,
            337921255,
            600817940,
            362280863,
            2060037813,
            574898872,
            1496822923,
            482801615,
            692907170,
            918315939,
            2146436496,
            567968469,
            53075866,
            2086105742,
            1830427308,
            2060155344,
            1544954453,
            440448624,
            402960803,
            682942238,
            656429291,
            779606085,
            969176928,
            336092061,
            1816183805,
            1043832898,
            214743081,
            1144555612,
            2121104547,
            972443269,
            937033015,
            757455951,
            925135436,
            772100271,
            1691191438,
            1056433013,
            464156732,
            1511942141,
            525519383,
            1455566762,
            983184707,
            731117222,
            560371665,
            1623793531,
            1584901137,
            1576164592,
            1208966014,
            294561940,
            1942360938,
            2103446125,
            789047573,
            1542796543,
            1571179903,
            2003207468,
            1772622408,
            1176757992,
            647954143,
            1065066589,
            1551359353,
            855817576,
            365443970,
            2053070402,
            214823433,
            561198753,
            1512049378,
            289424499,
            1741068653,
            1312102597,
            1097030543,
            1842148205,
            170747825,
            710561989,
            419370273,
            2117638032,
            10313137,
            1505320533,
            1630078988,
            1302566257,
            716404267,
            1916247301,
            578830286,
            2058512816,
            241461189,
            791337364,
            635878377,
            1745697754,
            1540208616,
            2133065144,
            1618864251,
            1522613513,
            103365104,
            8890836,
            177529382,
            1563175464,
            2091978632,
            669267608,
            2146384050,
            1188590309,
            1477370291,
            1872698984,
            929912440,
            282216730,
            1591541735,
            799675720,
            1209132099,
            162254204,
            2125496430,
            1551886312,
            2054341600,
            1504600335,
            1026087434,
            2120912606,
            1082387558,
            1090770519,
            632392902,
            11808541,
            1472416291,
            158325691,
            169335099,
            1636151153,
            1095108757,
            690414442,
            1561093148,
            1666541451,
            963850277,
            875557647,
            1978263065,
            1151836804,
            1629269877,
            8664682,
            2027202285,
            186547998,
            1067860124,
            714146279,
            1391804051,
            291383383,
            1849265327,
            321829080,
            1403111884,
            1447240245,
            757390175,
            291692873,
            1539710959,
            1729084479,
            1884095547,
            1654241165,
            1193947904,
            625125704,
            1990043745,
            1808484281,
            15321667,
            2114728223,
            259105341,
            933142165,
            1219669814,
            581763368,
            963587625,
            440353485,
            1799896010,
            401059566,
            1243538512,
            1333482017,
            1021488593,
            1212984914,
            1005556863,
            1041192498,
            65903743,
            1193950143,
            660121192,
            1245507175,
            1200687044,
            1621079715,
            1964058731,
            1158308494,
            1763437220,
            1570870571,
            };
        private readonly Dictionary<int, byte> _decryptionKeys;

        public EncryptorService()
        {
            _decryptionKeys = new Dictionary<int, byte>();
            for (int i = 0; i < _encryptionKeys.Length; i++)
            {
                _decryptionKeys.Add(_encryptionKeys[i], (byte)i);
            }
        }
    }
}
