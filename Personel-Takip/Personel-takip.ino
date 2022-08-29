#include <MFRC522.h>
#include <SPI.h>


int rst_pin=9;
int ss_pin=10;

MFRC522 rfid(ss_pin,rst_pin);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();

  
}

void loop() {
  // put your main code here, to run repeatedly:
  if(rfid.PICC_IsNewCardPresent())
  {
      if(rfid.PICC_ReadCardSerial())
      {
        Serial.print(rfid.uid.uidByte[0]);
        Serial.print(rfid.uid.uidByte[1]);
        Serial.print(rfid.uid.uidByte[2]);
        Serial.print(rfid.uid.uidByte[3]);
      
      }
      rfid.PICC_HaltA();
  }

}
