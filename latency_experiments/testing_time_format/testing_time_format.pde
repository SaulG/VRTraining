import java.text.SimpleDateFormat;
import java.text.ParseException;
import java.util.Date;

void draw(){
  Date d = new Date();
  String s = d.toString();
  SimpleDateFormat format = new SimpleDateFormat("EE MMM dd HH:mm:ss:SSS zzz yyyy");
  println(format.format(d));
}
