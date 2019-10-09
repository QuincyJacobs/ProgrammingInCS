using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDiagnostics
{
    public class Program
    {
        private static TraceSource configSource =
            new TraceSource("ConsoleAppDiagnostics");

        static void Main(string[] args)
        {
            // use 'w', 'a', 's', 'd' to move and generate debug and trace

            // comment next line for assert error
            GameState.Map = new Map(30, 25);

            GameState.Character = new Character(10, 10);
            GameState.Map.Draw();

            #region Trace listeners

            // console output
            TraceListener consoleListener = new ConsoleTraceListener();
            // delimited records by a ';', can be used for xls for example
            TraceListener delimitexListTraceListener = new DelimitedListTraceListener("logs_D-Limitor5000.txt");
            // event log requires you to register the event log in windows
            //EventLogTraceListener eventLogTraceListener = new EventLogTraceListener("EventSource");
            // windows event schema
            TraceListener eventSchemaTraceListener = new EventSchemaTraceListener("logs_SchemingEvent.xsd");
            // any text file
            TraceListener textWriterTraceListener = new TextWriterTraceListener("logs_Writ0r.txt");
            // xml
            TraceListener xmlWriterTraceListener = new XmlWriterTraceListener("logs_Xmlol.xml");

            // Add trace listeners to regular traces
            Trace.Listeners.Add(consoleListener);
            Trace.Listeners.Add(delimitexListTraceListener);
            Trace.Listeners.Add(eventSchemaTraceListener);
            Trace.Listeners.Add(textWriterTraceListener);
            Trace.Listeners.Add(xmlWriterTraceListener);

            #endregion

            #region Trace source and switch

            // switch trace level of the custom tracesource
            var control = new SourceSwitch("control", "Controls the tracing")
            {
                Level = SourceLevels.All
            };

            var trace = new TraceSource("Tray-zor", SourceLevels.All)
            {
                Switch = control
            };

            // switch trace level of the static "Trace"
            var traceSwitch = new TraceSwitch("General", "The entire application");
            traceSwitch.Level = TraceLevel.Verbose;

            // Add trace listeners to custom trace source
            trace.Listeners.Add(consoleListener);
            trace.Listeners.Add(delimitexListTraceListener);
            trace.Listeners.Add(eventSchemaTraceListener);
            trace.Listeners.Add(textWriterTraceListener);
            trace.Listeners.Add(xmlWriterTraceListener);

            // trace some events
            trace.TraceEvent(TraceEventType.Verbose, 1, "Using Trace Source");
            trace.TraceEvent(TraceEventType.Start, 10000, "Start of logical operation");
            trace.TraceEvent(TraceEventType.Warning, 20000, "Warning");
            trace.TraceEvent(TraceEventType.Verbose, 30000, "Debug");
            trace.TraceData(TraceEventType.Information, 1003, GameState.Character.Position);

            #endregion

            #region Trace through config

            EventTypeFilter configFilter =
                (EventTypeFilter)configSource.Listeners["console"].Filter;
            configSource.Switch.Level = SourceLevels.All;
            configSource.TraceEvent(TraceEventType.Verbose, 1, "Using config trace");

            #endregion

            #region Performance

            Stopwatch stopwatch = new Stopwatch();

            // this needs adminisrator rights
            if (!PerformanceCounterCategory.Exists("AverageCount64NewCounterOfMineCategory"))
            {

                CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();

                // Add the counter.
                CounterCreationData newCounterOfMine = new CounterCreationData();
                newCounterOfMine.CounterType = PerformanceCounterType.AverageCount64;
                newCounterOfMine.CounterName = "NewCounterOfMine";
                counterDataCollection.Add(newCounterOfMine);

                // Add another counter.
                CounterCreationData newerCounterOfMine = new CounterCreationData();
                newerCounterOfMine.CounterType = PerformanceCounterType.AverageBase;
                newerCounterOfMine.CounterName = "NewerCounterOfMine";
                counterDataCollection.Add(newerCounterOfMine);

                // Create the category.
                PerformanceCounterCategory.Create("AverageCount64NewCounterOfMineCategory",
                    "Demonstrates usage of the NewCounterCategory performance counter type.",
                    PerformanceCounterCategoryType.SingleInstance, counterDataCollection);
                Trace.WriteLine("Created loopCounter");
            }

            var loopCounter = new PerformanceCounter("AverageCount64NewCounterOfMineCategory",
             "NewerCounterOfMine",
             false)
            {
                RawValue = 0
            };

            var performance = new PerformanceCounter("Processor Information",
             "% Processor Time",
             "_Total");

            #endregion

            #region EventLog

            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("MySource"))
            {
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource("MySource", "MyNewLog");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");
                // The source is created.  Exit the application to allow it to be registered.
                return;
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "MySource";

            // Write an informational entry to the event log.    
            myLog.WriteEntry("Writing to event log.");

            Console.ForegroundColor = ConsoleColor.Green;
            foreach(EventLogEntry entry in myLog.Entries)
            {
                Console.WriteLine("Source: {0}, Type: {1}, Time: {2}, Message: {3}", entry.Source, entry.EntryType, entry.TimeWritten, entry.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;

            #endregion

            for (;;)
            {
                

                // trace
                configSource.TraceEvent(TraceEventType.Verbose, 191919, "Start of iteration of the eternal loop");
                trace.TraceEvent(TraceEventType.Verbose, 10000, "Start of iteration of the eternal loop");
                Trace.WriteLine("Iteration of the eternal input loop");

                // logic
                var read = Console.ReadKey().KeyChar;
                Console.Clear();
                stopwatch.Restart();
                GameState.Character.ProcessInput(read);
                GameState.Map.Draw();

                // stopwatch
                stopwatch.Stop();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\tms after processing input: {0}", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("\tTicks after processing input: {0}", stopwatch.ElapsedTicks);
                Console.WriteLine("\tTime after processing input: {0}", stopwatch.Elapsed);
                Console.ForegroundColor = ConsoleColor.White;
                stopwatch.Start();

                // trace
                Trace.WriteLine("Drawn the map");
                Trace.WriteLine("End of the iteration of the eternal loop");
                trace.TraceEvent(TraceEventType.Verbose, 10001, "End of iteration of the eternal loop");
                configSource.TraceEvent(TraceEventType.Verbose, 191920, "End of iteration of the eternal loop");

                // stopwatch
                stopwatch.Stop();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\tCumulative ms after 4 trace calls: {0}", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("\tCumulative ticks after 4 trace calls: {0}", stopwatch.ElapsedTicks);
                Console.WriteLine("\tCumulative time after 4 trace calls: {0}", stopwatch.Elapsed);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Yellow;

                // counters
                loopCounter.IncrementBy(20);
                Console.WriteLine("% of processor time: {0}", performance.NextValue());
                Console.WriteLine("Our own counter: {0}", loopCounter.NextValue());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
