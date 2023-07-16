using System;
using System.Collections.Generic;
using System.Linq;

// Interface for Book repository
public interface IBookRepository
{
    Book GetBookById(int id);
    void AddBook(Book book);
    void RemoveBook(Book book);
}

// Interface for Member repository
public interface IMemberRepository
{
    Member GetMemberById(int id);
    void AddMember(Member member);
    void RemoveMember(Member member);
}

// Book class
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public override string ToString()
    {
        return $"{Id}: {Title} - {Author}";
    }
}

// Member class
public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Id}: {Name}";
    }
}

// Library class responsible for managing books and members
public class Library
{
    private readonly IBookRepository bookRepository;
    private readonly IMemberRepository memberRepository;

    public Library(IBookRepository bookRepository, IMemberRepository memberRepository)
    {
        this.bookRepository = bookRepository;
        this.memberRepository = memberRepository;
    }

    public void AddBook(Book book)
    {
        bookRepository.AddBook(book);
    }

    public void RemoveBook(Book book)
    {
        bookRepository.RemoveBook(book);
    }

    public void AddMember(Member member)
    {
        memberRepository.AddMember(member);
    }

    public void RemoveMember(Member member)
    {
        memberRepository.RemoveMember(member);
    }

    // Other library management methods
}

// LibraryService class responsible for handling user inputs and interactions
public class LibraryService
{
    private readonly Library library;

    public LibraryService(Library library)
    {
        this.library = library;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the Library Management System!");

        while (true)
        {
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Member");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    AddMember();
                    break;
                case "3":
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddBook()
    {
        Console.WriteLine("Enter book details:");
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();

        var book = new Book { Title = title, Author = author };
        library.AddBook(book);
        Console.WriteLine("Book added successfully!");
    }

    private void AddMember()
    {
        Console.WriteLine("Enter member details:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        var member = new Member { Name = name };
        library.AddMember(member);
        Console.WriteLine("Member added successfully!");
    }
}

// Concrete implementation of BookRepository using in-memory list
public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> books = new List<Book>();

    public Book GetBookById(int id)
    {
        return books.Find(book => book.Id == id);
    }

    public void AddBook(Book book)
    {
        int maxId = books.Count == 0 ? 0 : books.Max(b => b.Id);
        book.Id = maxId + 1;
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.RemoveAll(b => b.Id == book.Id);
    }
}

// Concrete implementation of MemberRepository using in-memory list
public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> members = new List<Member>();

    public Member GetMemberById(int id)
    {
        return members.Find(member => member.Id == id);
    }

    public void AddMember(Member member)
    {
        int maxId = members.Count == 0 ? 0 : members.Max(m => m.Id);
        member.Id = maxId + 1;
        members.Add(member);
    }

    public void RemoveMember(Member member)
    {
        members.RemoveAll(m => m.Id == member.Id);
    }
}

public class Program
{
    public static void Main()
    {
        var bookRepository = new InMemoryBookRepository();
        var memberRepository = new InMemoryMemberRepository();
        var library = new Library(bookRepository, memberRepository);
        var libraryService = new LibraryService(library);
        libraryService.Run();
    }
}
