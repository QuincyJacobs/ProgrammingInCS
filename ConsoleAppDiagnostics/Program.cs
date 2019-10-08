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

            SourceSwitch control = new SourceSwitch("control", "Controls the tracing")
            {
                Level = SourceLevels.All
            };

            TraceSource trace = new TraceSource("Tray-zor", SourceLevels.All)
            {
                Switch = control
            };

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

            for (;;)
            {
                // trace
                configSource.TraceEvent(TraceEventType.Verbose, 191919, "Start of iteration of the eternal loop");
                trace.TraceEvent(TraceEventType.Verbose, 10000, "Start of iteration of the eternal loop");
                Trace.WriteLine("Iteration of the eternal input loop");

                // logic
                var read = Console.ReadKey().KeyChar;
                GameState.Character.ProcessInput(read);
                GameState.Map.Draw();

                // trace
                Trace.WriteLine("Drawn the map");
                Trace.WriteLine("End of the iteration of the eternal loop");
                trace.TraceEvent(TraceEventType.Verbose, 10001, "End of iteration of the eternal loop");
                configSource.TraceEvent(TraceEventType.Verbose, 191920, "End of iteration of the eternal loop");
            }
        }
    }
}
