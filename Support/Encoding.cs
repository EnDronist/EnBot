using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBot.Support {
    public static class Encoding {
        public static class UTF6 {
            public static byte[] FromUTF8(byte[] bytes) {
                byte[] utf8bytes = new byte[bytes.Length];
                Array.Copy(bytes, utf8bytes, bytes.Length);
                // Очистка первых двух битов
                utf8bytes = utf8bytes.Select(elem => (byte)(elem & 0x3F)).ToArray();
                // Формирование UTF6 строки в байтах
                var resultBytes = new List<byte>();
                // Сложная версия алгоритма (не доделано)
                /*int currentShift = 0;
                byte lastByte = 0;
                for (var i = utf8bytes.Length - 1; i >= 0; i--) {
                    currentShift += 2;
                    var currentByte = utf8bytes[i];
                    var kek1 = (byte)(currentByte >> (8 - currentShift));
                    var kek2 = (byte)(lastByte << currentShift);
                    resultBytes.Add((byte)
                        ((byte)(currentByte >> 8 - currentShift)
                            | (byte)(lastByte << currentShift))
                    );
                    if (currentShift >= 8)
                        currentShift -= 8;
                    lastByte = currentByte;
                }*/
                // Простая версия алгоритма
                // Преобразование битов в булевые переменные
                var bitBooleans = new List<bool>();
                foreach (var currentByte in utf8bytes) {
                    for (var i = 2; i < 8; i++) {
                        if ((currentByte & (0x80 >> i)) != 0)
                            bitBooleans.Add(true);
                        else bitBooleans.Add(false);
                    }
                }
                // Обратное преобразование булевых переменных в биты
                int bitCounter = 0;
                for (var i = bitBooleans.Count - 1; i >=0; i--) {
                    var bit = bitBooleans[i];
                    // Создание нового байта
                    if (bitCounter == 0) {
                        resultBytes.Insert(0, 0);
                    }
                    // Изменение бита текущего байта
                    if (bit)
                        resultBytes[0] = (byte)(
                            resultBytes[0] | (0x1 << bitCounter)
                        );
                    // Сброс счётчика при переполнении
                    bitCounter++;
                    if (bitCounter == 8) {
                        bitCounter = 0;
                    }
                }
                return resultBytes.ToArray();
            }
        }
    }
}