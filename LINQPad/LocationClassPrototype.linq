<Query Kind="Program" />

void Main()
{
	
}

// Define other methods and classes here000.

public class Location
{
	public int LocationId {get; set;}
	public Address Address {get; set;}
	public List<Note> Notes {get; set;}
}

public class Address
{
	public int AddressId {get;set;}
	public String Address1 {get; set;}
	public String Address2 {get; set;}
	public String Address3 {get; set;}
	public String City {get;set;}
	public State State {get; set;}
	public ZipPostalCode ZipPostalCode {get;set;}
	public Coordinates Coordinates {get;set;}
}

public class ZipPostalCode
{
	public int ZipCode {get;set;}
	public int Plus4 {get;set;}
}

public class State
{
	public String Abbreviation {get;set;}
	public String Literal {get;set;}
}

public class Coordinates
{
	public decimal Latitude {get; set;}
	public decimal Longitude {get;set;}
}

public class Note
{
	public int id {get;set;}
	public String Body {get; set;}
}