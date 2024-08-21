const int BUTTON_PIN = 7; 
const int LED_PIN =  3;
const int BUFFER_SIZE = 50;

char buf[BUFFER_SIZE];
int buttonState = 0; 

void setup() {
  Serial.begin(9600);
  pinMode(LED_PIN, OUTPUT);
  pinMode(BUTTON_PIN, INPUT_PULLUP);
}

void loop() {
  readDataFromUnity();
  sendDataToUnity();
}

void readDataFromUnity() {
  if (Serial.available() > 0) {
      int len = Serial.readBytes(buf, BUFFER_SIZE);

      if (buf[0] == '0') {
        digitalWrite(LED_PIN, LOW);
      } else if (buf[0] == '1') {
        digitalWrite(LED_PIN, HIGH);
      }
    }
}

void sendDataToUnity() {
  int buttonValue = digitalRead(BUTTON_PIN);
  buttonValue = !buttonValue;
  Serial.println(buttonValue);
  delay(200);
}