using Structure;
using System.ComponentModel;
using static Structure.RailRoadPath;

namespace RailRoadStationModelTests;
// 6. Unit тесты для схемы станции
public class RailRoadStationModelTests
{
    private Vertex a, b, c, d, e, f, g, h, i, j, k, l;
    private RailRoadPath green, violet, white, red;
    private List<(Vertex, Vertex)> greenEdges, violetEdges, whiteEdges, redEdges;
    private Park allInPark = new Park("allInPark");
    private Park greenAndRedPark = new Park("greenAndRedPark");
    private Park violetAndWhitePark = new Park("violetAndWhitePark");
    private List<Park> parksList = new List<Park>();
    public RailRoadStationModelTests() {
        // Arrange common data
        a = new Vertex(3, 0);
        b = new Vertex(3, 1);
        c = new Vertex(3, 4);
        d = new Vertex(4, 5);
        e = new Vertex(4, 6);
        f = new Vertex(3, 6);
        g = new Vertex(2, 0);
        h = new Vertex(2, 2);
        i = new Vertex(2, 3);
        j = new Vertex(2, 4);
        k = new Vertex(2, 6);
        l = new Vertex(1, 5);

        green = new RailRoadPath("green");
        violet = new RailRoadPath("violet");
        white = new RailRoadPath("white");
        red = new RailRoadPath("red");

        greenEdges = new List<(Vertex, Vertex)> { (a, b), (b, c), (c, f) };
        violetEdges = new List<(Vertex, Vertex)> { (a, b), (b, h), (h, i), (i, j), (j, l) };
        whiteEdges = new List<(Vertex, Vertex)> { (g, h), (h, i), (i, c), (c, d), (d, e) };
        redEdges = new List<(Vertex, Vertex)> { (g, h), (h, i), (i, j), (j, k) };

        foreach (var (from, to) in greenEdges)
            green.AddEdge(from, to);
        foreach (var (from, to) in violetEdges)
            violet.AddEdge(from, to);
        foreach (var (from, to) in whiteEdges)
            white.AddEdge(from, to);
        foreach (var (from, to) in redEdges)
            red.AddEdge(from, to);

        var graphs = new[] { green, violet, white, red };
        foreach (var graph in graphs)
            allInPark.AddGraph(graph);

        greenAndRedPark.AddGraph(green);
        greenAndRedPark.AddGraph(red);

        violetAndWhitePark.AddGraph(violet);
        violetAndWhitePark.AddGraph(white);

        parksList.Add(allInPark);
        parksList.Add(greenAndRedPark);
        parksList.Add(violetAndWhitePark);
    }
    [Fact]
    public void TestConvexHullVertices() {
        // Act
        var convexHull = allInPark.ComputeConvexHull();
        // Assert
        var expectedHull = new List<Vertex> { a, g, l, k, e, d };
        Assert.Equal(expectedHull, convexHull);
    }
    [Theory]
    [InlineData(1, 1, 3, 5)]
    public void FindEdgesBetweenVertices_ValidPath(int parkId, int startVertexId, int endVertexId, double expectedWeight) {
        // Assert
        Park currentpark = parksList[parkId];
        Vertex startVertex = currentpark.GetVertices()[startVertexId];
        Vertex endVertex = currentpark.GetVertices()[endVertexId];
        // Act
        (double shortestPathWeight, List<Edge> pathEdges) = currentpark.FindEdgesBetweenVertices(startVertex, endVertex);
        // Assert
        Assert.Equal(expectedWeight, shortestPathWeight);
    }

}