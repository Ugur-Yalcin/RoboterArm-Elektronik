
#include <VarSpeedServo.h>

VarSpeedServo servo1;
VarSpeedServo servo2;
VarSpeedServo servo3;
VarSpeedServo servo4;
VarSpeedServo servo5;

// Degiskenlerimiz
int userInput[3];    // raw input from serial buffer, 3 bytes
int startbyte;       // start byte, begin reading input
int servo;           // which servo to pulse?
int pozisyon;             // servo angle 0-180

//Komut Arrayalari
int veri[] = {190, 473, 55, 290, 4110, 274, 5135, 4142, 2125, 482, 2170, 1161, 295, 4118, 270, 55, 2170, 448, 190, 5135};
int al[] = {5135, 190, 448, 2170, 55, 270, 4118, 295, 1161, 2170, 482, 2125, 4142, 5135, 274, 4110, 290, 55, 473, 190};
int dik_dur[] = {196, 285, 396, 460, 500, 590};
int l_konum[] = {195, 2170, 476, 3180, 560};

void setup()
{
  servo1.attach(2);
  servo2.attach(3);
  servo3.attach(4);
  servo4.attach(5);
  servo5.attach(6);
  islem(dik_dur, sizeof(dik_dur));
  Serial.begin(9600);

}

//Burada Serial Port'dan gelen verilere göre islem yapiliyor
void loop()
{
  Serial.flush();
  // Wait for serial input (min 3 bytes in buffer)
  if (Serial.available() > 2) {
    // Read the first byte
    startbyte = Serial.read();
    //Serial.print(startbyte);
    // If it's really the startbyte (ASCII 35 -> #)
    if (startbyte == 35) {
      // ... then get the next two bytes
      for (int i = 0; i < 2; i++) {
        userInput[i] = Serial.read();
      }
      // First byte = servo to move?
      servo = userInput[0];
      // Second byte = which pozisyonition?
      pozisyon = userInput[1];

      // Packet error checking and recovery
      if (pozisyon == 255) {
        servo = 255;
      }

      // Assign new pozisyonition to appropriate servo
      switch (servo) {
        case 1:
          servo1.write(pozisyon, 40, true);  // move servo1 to 'pozisyon'
          break;
        case 2:
          servo2.write(pozisyon, 40, true);
          break;
        case 3:
          servo3.write(pozisyon, 40, true);
          break;
        case 4:
          servo4.write(pozisyon, 40, true);
          break;
        case 5:
          servo5.write(pozisyon); // Gripper hizli olmasi gerek
          break;
        case 6:  //Objeyi al getir funktionu icin
          islem(veri, sizeof(veri));
          break;
        case 7:  //Standart Pozition al funktionu
          islem(al, sizeof(al));
          break;
        case 8:  //Dikdur Pozition al funksiyonu
          islem(dik_dur, sizeof(dik_dur));
          break;
        case 9:  //L Pozition al funksiyonu
          islem(l_konum, sizeof(l_konum));
          break;
      }
    }
  }

}

void islem(int komut[], int deger)    //Burdaki funktionun paremetresi bir int array
{
  String konum;
  String gelen_veri;
  String servonumm;

  for (int i = 0; i < deger / 2; i++)
  {
    gelen_veri = String(komut[i]);                         //Gelen veriyi string islemler yapabilmek icin stringe cevirioyruz
    servonumm = gelen_veri.substring(0, 1);                //Ilk karekteri aliyoruz
    konum = gelen_veri.substring(1, 4);                    //Ilk karekterden sonraki karekterleri aliyoruz
    yap(servonumm, konum.toInt());                         //konum degiskeninini tekrar int'e ceviriyoruz
  }

}

int yap(String servo_nummer, int pozisyon)
{
  //Serial.print(servo_nummer + " - " + pozisyon);
  //Serial.println();
  switch (servo_nummer.toInt()) {
    case 1:
      servo1.write(pozisyon, 40, true);
      break;
    case 2:
      servo2.write(pozisyon, 40, true);
      break;
    case 3:
      servo3.write(pozisyon, 40, true);
      break;
    case 4:
      servo4.write(pozisyon, 40, true);
      break;
    case 5:
      servo5.write(pozisyon);
      break;

  }
}
