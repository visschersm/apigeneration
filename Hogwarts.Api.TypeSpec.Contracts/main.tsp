import "@typespec/http";

using TypeSpec.Http;

@service({
    title: "Hogwarts API",
})
@server("https://example.com", "Single server endpoint")
namespace Hogwarts.Contracts;

model Student {
    id: int32;

    @minLength(1)
    fullname: string;
}

@route("/students")
namespace Students {
    @get
    op listStudents(): {
        @statusCode statusCode: 200;
        @body students: Student[];
    };

    @get
    op getStudent(@path studentId: int32): {
        @statusCode statusCode: 200;
        @body student: Student;
    } | {
        @statusCode statusCode: 404;
    };
}
