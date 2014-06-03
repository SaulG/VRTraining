import java.text.SimpleDateFormat;
import java.text.ParseException;
import java.util.Date;

public Date d;

void setup(){
 d = new Date();
}

void draw(){
  String s = d.toString();
  SimpleDateFormat format = new SimpleDateFormat("MMM dd yyyy HH:mm:ss:SSS");
  println(format.format(d));
}
