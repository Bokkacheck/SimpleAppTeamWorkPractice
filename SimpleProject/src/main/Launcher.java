package main;

import com.google.gson.Gson;
import models.Data;
import models.Person;
import spark.ModelAndView;
import spark.template.handlebars.HandlebarsTemplateEngine;

import javax.servlet.MultipartConfigElement;
import java.io.InputStream;
import java.lang.reflect.Array;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.util.ArrayList;
import java.util.HashMap;

import static spark.Spark.*;

public class Launcher {
    public static void main(String[] args) {
        staticFiles.location("/public");

        port(5000);
        String path="src/public/json/people.json";

        HashMap<String,Object> polja=new HashMap<>();

        get("/api/getPeople",(request, response) -> {
            return new Gson().toJson(Data.readFromJson(path));
        });
        post("/api/addPerson",(request, response) -> {
            System.out.println(request.contentType());
            System.out.println(request.queryParams().size());
            return true;
        });

        post("/newPerson",(request, response) -> {
            int id = Integer.parseInt(request.queryParams("id"));
            String firstName = request.queryParams("firstName");
            String lastName = request.queryParams("lastName");
            int age = Integer.parseInt(request.queryParams("age"));
            ArrayList<Person> people = Data.readFromJson(path);
            people.add(new Person(id,firstName,lastName,age));
            Data.writeToJSON(people,path);
            return new Gson().toJson("true");
        });
        ArrayList<Person> people = new ArrayList<>();
        people.add(new Person(1,"Bojan","Stojkovic",22));
        people.add(new Person(2,"Marko","Zivojinovic",21));
        Data.writeToJSON(people,path);
    }
}
