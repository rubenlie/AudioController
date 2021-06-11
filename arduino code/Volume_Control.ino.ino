
long valStabel = 0;
long potval = 0;
int potvalold = 0;
bool conf = true;
int intsetup = 111;
int printint = 0;


void setup() {
  Serial.begin(9600); 
}


void loop() {

  if (conf == false)
  {
    Serial.println(intsetup);
    }
  if (conf == true){
 potval = analogRead(A1);
 valStabel +=  (potval - valStabel)/4;
 potval = (valStabel*100/1020);

 
 
 if (potvalold != potval)
 {
  printint = 100-potval;
  Serial.println(printint);
   potvalold = (valStabel*100/1020);
 
  
  }
  }
 
 
}
